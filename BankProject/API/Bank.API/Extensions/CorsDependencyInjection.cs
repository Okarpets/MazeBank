namespace Bank.API.Extensions
{
    public static class CorsDependencyInjection
    {
        public static void AddReactCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", policy =>
                {
                    policy.WithOrigins("https://mazebankclient-cpcadyduaafrghf0.westeurope-01.azurewebsites.net",
                            "https://idontknowtheirshopurl.azurewebsites.net")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }
    }
}