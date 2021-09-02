using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Coworking.Integration.Tests.Get
{
    public class OrderGetTests : IClassFixture<AuthenticationApplicationFactory<Startup>>,
        IAssemblyFixture<StringyEnumFixture>
    {
        private readonly AuthenticationApplicationFactory<Startup> _factory;

        public OrderGetTests(AuthenticationApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("/order/get")]
        public async Task GetAll_AssertStatusCode(string uri)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(uri);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_AssertStatusCode_NotFound()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("order/get/1");

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}