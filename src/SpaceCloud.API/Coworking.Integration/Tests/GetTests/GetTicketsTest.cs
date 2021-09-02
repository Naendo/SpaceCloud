using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Coworking.Integration.Tests.Get
{
    public class GetTicketsTests : IAssemblyFixture<StringyEnumFixture>,
        IClassFixture<AuthenticationApplicationFactory<Startup>>

    {
        private readonly AuthenticationApplicationFactory<Startup> _factory;


        public GetTicketsTests(AuthenticationApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/ticket/get")]
        public async Task GetAll_AssertStatusCode(string uri)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(uri);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}