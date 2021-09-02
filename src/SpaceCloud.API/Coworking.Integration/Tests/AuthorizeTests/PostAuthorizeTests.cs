using System;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Coworking.Integration.Tests
{
    public class PostAuthorizeTests : IClassFixture<AuthenticationApplicationFactory<Startup>>,
        IAssemblyFixture<StringyEnumFixture>
    {
        private readonly AuthenticationApplicationFactory<Startup> _factory;
        private readonly WebApplicationFactoryClientOptions _options;

        public PostAuthorizeTests(AuthenticationApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false,
                HandleCookies = true,
                BaseAddress = new Uri("https://localhost:5001")
            };
        }
    }
}