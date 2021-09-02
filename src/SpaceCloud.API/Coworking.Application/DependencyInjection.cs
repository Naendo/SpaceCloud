using Coworking.Application.Interfaces;
using Coworking.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Wkhtmltopdf.NetCore;

namespace Coworking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, AuthenticationOptions options)
        {
            services.AddScoped<ITokenGenerator, PasswordTokenFactory>();
            services.AddTransient<IHashService>(factory => new HashService(options.Pepper));
            services.AddScoped<InvoiceFactory>();


            return services;
        }

        public static IServiceCollection AddMailService(this IServiceCollection services)
        {
            services.AddWkhtmltopdf();
            services.AddSingleton<MailService>();
            return services;
        }

        public static IServiceCollection AddBlobService(this IServiceCollection services)
        {
            services.AddSingleton<BlobService>();
            return services;
        }

        public static IServiceCollection AddAvailabilityService(this IServiceCollection services)
        {
            services.AddSingleton<AvailabilityService>();
            return services;
        }
    }
}