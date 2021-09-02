using System.Net;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Coworking.Integration.Tests.Get
{
    public class GetUserTests : IClassFixture<AuthenticationApplicationFactory<Startup>>,
        IAssemblyFixture<StringyEnumFixture>
    {
        private readonly AuthenticationApplicationFactory<Startup> _factory;

        public GetUserTests(AuthenticationApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Theory]
        [InlineData("/user/get")]
        public async Task GetAll_AssertStatusCode_Ok(string uri)
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync(uri);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetById_AssertStatusCode_Ok()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync("user/get/" + TestEntity.User.UserId);

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}