using Coworking.Application.Interfaces;
using Coworking.Infrastructure.Persistence;
using Coworking.Integration.Tests.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Coworking.Integration.Tests.BaseFactory
{
    public class BaseApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class

    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);

            builder.ConfigureServices(services =>
            {
                services.AddDbContext<WorkingContext>(options => { options.UseInMemoryDatabase("InMemoryDatabase"); });

                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<WorkingContext>();
                    var hashService = scopedServices.GetRequiredService<IHashService>();

                    TestUtilties.InitializeDbForTests(db, hashService);
                }
            });

            builder.UseEnvironment("Integration");
        }
    }
}