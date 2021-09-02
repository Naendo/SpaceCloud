using Coworking.Integration.Tests.BaseFactory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace Coworking.Integration.Tests
{
    public class AuthenticationApplicationFactory<TStartup>
        : BaseApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>(
                        "Test", options => { });
            });

            base.ConfigureWebHost(builder);
        }
    }
}