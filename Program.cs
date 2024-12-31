
using FluentValidation;
using Medallion.Threading.Postgres;
using Medallion.Threading;
using Microsoft.EntityFrameworkCore;
using StreetService.Behaviors;
using StreetService.Data;
using StreetService.Middleware;
using System.Reflection;
using System.Text.Json.Serialization;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO.Converters;

namespace StreetService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    var geoJsonConverterFactory = new GeoJsonConverterFactory();
                    options.JsonSerializerOptions.Converters.Add(geoJsonConverterFactory);
                });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Add health checks
            builder.Services.AddHealthChecks();

            // For DistributedLock
            _ = builder.Services.AddSingleton<IDistributedLockProvider>(_ => new PostgresDistributedSynchronizationProvider(builder.Configuration.GetConnectionString("StreetDbConnection"),
                opt =>
                {
                    opt.UseTransaction(true);
                }));

            // Add FluentValidation
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Add MediatR
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<Program>();
                // Pipeline behaviour for using fluentvalida6ations
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            // Add DBContext
            builder.Services.AddDbContext<StreetDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("StreetDbConnection"),
                o =>
                {
                    o.UseNetTopologySuite();
                });
            });

            // Add ExceptionHandlingMiddleware
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.MapHealthChecks("/healthz");
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
