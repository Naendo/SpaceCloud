using Coworking.Application.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace Coworking.Application.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCache(this IServiceCollection services, string connectionString)
        {
            var redisCacheSettings = new RedisCacheSettings
            {
                ConnectionString = connectionString,
                Enabled = true
            };
            services.AddSingleton<RedisCacheSettings>();

            if (!redisCacheSettings.Enabled) return services;

            services.AddStackExchangeRedisCache(options => options.Configuration = redisCacheSettings.ConnectionString);
            services.AddSingleton<IResponseCacheService, ResponseCacheService>();

            return services;
        }
    }
}