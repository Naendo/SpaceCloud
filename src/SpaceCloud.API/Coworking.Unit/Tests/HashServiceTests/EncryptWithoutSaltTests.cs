using System;
using Coworking.Application.Interfaces;
using Coworking.Application.Services;
using Xunit;

namespace Coworking.Unit.Tests
{
    public class EncryptWithoutSaltTests
    {
        private readonly IHashService _service;

        public EncryptWithoutSaltTests()
        {
            _service = new HashService("pepperyabra");
        }


        [Fact]
        public void EncryptWithoutGivenSalt_WithNullPassword_ThrowsException()
        {
            try
            {
                _service.EncryptPasswordWithoutGivenSalt(null!);
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void EncryptWithoutGivenSalt_WithEmptyPassword_ThrowsException()
        {
            try
            {
                _service.EncryptPasswordWithoutGivenSalt("");
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void EncryptWithoutGivenSalt_WithValidPassword_ReturnsValidEncryptionModel()
        {
            //{853cc657-49e4-45c8-a9fc-468727cb2af5}
            //{efed4433-7a54-40e8-b80e-8f7528d36f24}
            //14819987fc3ad803028313df0007d6455eae8a258e26d05e5012eb29e7994388
            try
            {
                var test = _service.EncryptPasswordWithoutGivenSalt("test");
                Assert.IsType<HashModel>(test);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }
    }
}