using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Integration.Tests.BaseFactory;
using Newtonsoft.Json;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

namespace Coworking.Integration.Tests.Login
{
    public class AuthorizeTests : IAssemblyFixture<StringyEnumFixture>,
        IClassFixture<BaseApplicationFactory<Startup>>
    {
        private readonly BaseApplicationFactory<Startup> _factory;


        public AuthorizeTests(BaseApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostAuthorize_AssertStatusCode()
        {
            var client = _factory.CreateClient();

            var validCredentials = new InputAuthorizeDto
            {
                Mail = TestEntity.Identity.Mail,
                Password = "test",
                StayLogged = true
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(validCredentials), Encoding.UTF8,
                "application/json");

            //Act
            var response = await client.PostAsync("/authorize", httpContent);

            //Assert
            Assert.NotNull(response.Content);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}