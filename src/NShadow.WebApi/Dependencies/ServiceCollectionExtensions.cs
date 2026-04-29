using NShadow.Core.Options;
using Serilog;

namespace NShadow.WebApi.Dependencies;

internal static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public void ConfigureServices(IWebHostEnvironment environment, IConfiguration configuration)
        {
            services.AddHealthChecks();
            services.AddRoutingCore();
            services.AddLogging(environment, configuration);

            services.Configure<ShadowOptions>(configuration.GetSection(nameof(ShadowOptions)));
        }

        internal IServiceCollection AddLogging(IWebHostEnvironment environment, IConfiguration configuration)
        {
            services.AddSerilog((innerServices, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(configuration)
                    .ReadFrom.Services(innerServices)
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("Application", "NShadow")
                    .Enrich.WithProperty("Environment", environment.EnvironmentName);
            });

            return services;
        }
    }
}