using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;

namespace Barbearia.API.Configurations
{
    public static class RateLimitingConfig
    {
        public static IServiceCollection AddRateLimitingServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Carregue as regras de IpRateLimit do appsettings.json
            services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));

            // Carregue as políticas de IpRateLimit do appsettings.json
            services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));

            // Injetar contadores de memória
            services.AddMemoryCache();
            services.AddInMemoryRateLimiting();

            // Injetar configurações
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

            return services;
        }
    }
}