using Entities;
using Entities.Models;
using HumanResource.Controllers;
using HumanResource.Infrastructure;
using LoggerService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository;

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

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>))
                .AddTransient<IRepositoryManager, RepositoryManager>()
                ;
        }

        public static void ConfigureSwgger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Human Resource API",
                    Version = "v1"
                });
                s.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Human Resource API",
                    Version = "v2"
                });
            });
        }
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;//save versioning reponse header
                opt.AssumeDefaultVersionWhenUnspecified = true;//if not assume 2 auto 1
                opt.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //opt.Conventions.Controller<CompaniesController>().HasApiVersion(1, 0);
                //opt.Conventions.Controller<CompaniesV2Controller>().HasApiVersion(2, 0);
            });
            services.AddApiVersioning();
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 10;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }
    }
}
