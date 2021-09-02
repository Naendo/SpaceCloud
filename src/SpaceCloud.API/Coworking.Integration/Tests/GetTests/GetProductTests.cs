using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Coworking.Integration.Tests.Get
{
    public class GetProductTests : IClassFixture<AuthenticationApplicationFactory<Startup>>,
        IAssemblyFixture<StringyEnumFixture>
    {
        private readonly AuthenticationApplicationFactory<Startup> _factory;

        public GetProductTests(AuthenticationApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("product/room/get")]
        [InlineData("product/desk/get")]
        public async Task GetAll_AssertStatusCode(string uri)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(uri);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Theory]
        [InlineData("product/room/get/1")]
        [InlineData("product/desk/get/1")]
        public async Task GetById_AssertStatusCode_NotFound(string uri)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(uri);

            //Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}