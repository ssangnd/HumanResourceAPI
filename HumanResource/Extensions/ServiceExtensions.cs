using Entities;
using HumanResource.Infrastructure;
using LoggerService;
using Microsoft.EntityFrameworkCore;

namespace HumanResource.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opstion =>
            {
                opstion.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services) {
            services.Configure<IISOptions>(options =>
            {

            });    
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opts => 
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
            b=>b.MigrationsAssembly("HumanResource")));
        }

    }
}
