
using Microsoft.EntityFrameworkCore;
using StreetService.Data;

namespace StreetService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<Program>();
                // Pipeline behaviour for using fluentvalidations
                //config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            builder.Services.AddDbContext<StreetDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("StreetDbConnection"),
                o =>
                {
                    o.UseNetTopologySuite();
                });
            });

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
