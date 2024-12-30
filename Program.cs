
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

            // For DistributedLock
            _ = builder.Services.AddSingleton<IDistributedLockProvider>(_ => new PostgresDistributedSynchronizationProvider(builder.Configuration.GetConnectionString("StreetDbConnection"),
                opt =>
                {
                    opt.UseTransaction(true);
                }));

            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<Program>();
                // Pipeline behaviour for using fluentvalida6ations
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });

            builder.Services.AddDbContext<StreetDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("StreetDbConnection"),
                o =>
                {
                    o.UseNetTopologySuite();
                });
            });
            builder.Services.AddTransient<ExceptionHandlingMiddleware>();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
