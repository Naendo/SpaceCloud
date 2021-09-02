using Coworking.Infrastructure.Persistence;
using Coworking.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Coworking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //Handle IntegrationTesting


            services.AddMediatR(typeof(AuthorizeQuery).Assembly);

            services.AddScoped<TestRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<AuthorizationRepository>();
            services.AddScoped<AccountRepository>();
            services.AddScoped<RefreshRepository>();
            services.AddScoped<DashboardRepository>();
            services.AddScoped<TicketRepository>();
            services.AddScoped<RecentActionRepository>();
            services.AddScoped<ProductRepository>();
            services.AddScoped<UserRepository>();
            services.AddScoped<InvoiceRepository>();
            services.AddScoped<OrderRepository>();
            services.AddScoped<SettingsRepository>();
            services.AddScoped<SchedulerRepository>();
            services.AddScoped<CompanyRepository>();
            services.AddScoped<CartRepository>();
            services.AddScoped<LocationRepository>();

            return services;
        }
    }
}