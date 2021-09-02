using System.Net;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Integration.Tests.BaseFactory;
using Coworking.Integration.Tests.Infrastructure;
using Xunit;
using Xunit.Extensions.AssemblyFixture;

[assembly: TestFramework(AssemblyFixtureFramework.TypeName, AssemblyFixtureFramework.AssemblyName)]

namespace Coworking.Integration.Tests.Login
{
    [Collection(TestUtilties.TEST_ORDERER_KEY + "A")]
    public class AccountCreatedTests : IClassFixture<BaseApplicationFactory<Startup>>,
        IAssemblyFixture<StringyEnumFixture>
    {
        private readonly BaseApplicationFactory<Startup> _factory;

        public AccountCreatedTests(BaseApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task PostCreateAccount_AssertStatusCode()
        {
            //Arrange
            var client = _factory.CreateClient();
            //Dont Change, Credentials requested in AuthorizeTests
            var obj = new RegisterDto
            {
                FirstName = "TestUser2",
                LastName = "TestUser2",
                Mail = "test.user@mail.com",
                Password = "test"
            };

            var content = TestUtilties.InitializeContent(obj);

            //Act
            var result = await client.PostAsync("account/register", content);


            //Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        }
    }
}