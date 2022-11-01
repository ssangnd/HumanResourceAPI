namespace HumanResource.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(opstion =>
            {
                opstion.AddPolicy("CorePolicy", builder =>
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
    }
}
