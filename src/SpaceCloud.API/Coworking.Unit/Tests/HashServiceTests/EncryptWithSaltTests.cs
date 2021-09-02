using System;
using Coworking.Application.Interfaces;
using Coworking.Application.Services;
using Xunit;

namespace Coworking.Unit.Tests
{
    public class EncryptWithSaltTests
    {
        private readonly HashModel _model;
        private readonly IHashService _service;

        public EncryptWithSaltTests()
        {
            _service = new HashService("pepperyabra");
            _model = new HashModel
            {
                Password = "ValidPassword",
                Salt = Guid.NewGuid()
            };
        }


        [Fact]
        public void EncryptWithGivenSalt_WithEmptyGuid_ThrowsException()
        {
            try
            {
                _model.Salt = Guid.Empty;
                _service.EncryptPasswordWithGivenSalt(_model);
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void EncryptWithGivenSalt_WithNullPassword_ThrowsException()
        {
            try
            {
                _model.Password = null;
                _service.EncryptPasswordWithGivenSalt(_model);
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void EncryptWithGivenSalt_WithValidCredentials_ReturnsValidHashModel()
        {
            var result = _service.EncryptPasswordWithGivenSalt(new HashModel
            {
                Password = "test",
                Salt = Guid.Parse("{efed4433-7a54-40e8-b80e-8f7528d36f24}")
            });

            Assert.Equal("14819987fc3ad803028313df0007d6455eae8a258e26d05e5012eb29e7994388", result.Password);
        }
    }
}