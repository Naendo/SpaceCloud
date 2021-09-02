using Coworking.Application.Services;
using Xunit;

namespace Coworking.Unit.Tests
{
    public class PasswordTokenFactoryTests
    {
        private readonly PasswordTokenFactory _service;


        public PasswordTokenFactoryTests()
        {
            _service = new PasswordTokenFactory();
        }


        [Fact]
        public void GenerateResetToken_WithUserIdentity_ReturnsMappedUserIdentity()
        {
            var test = _service.GenerateAndSetPasswordResetToken();


            Assert.NotNull(test.Token);
        }
    }
}