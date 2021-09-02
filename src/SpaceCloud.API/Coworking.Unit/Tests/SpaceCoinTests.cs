using System.Text;
using Coworking.Application;
using Coworking.Application.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace Coworking.Unit.Tests
{
    public class SpaceCoinTests
    {
        private readonly CurrencyService _service;

        public SpaceCoinTests()
        {
            _service = new CurrencyService(new OptionsWrapper<CurrencyOptions>(new CurrencyOptions
            {
                CurrencySecret = "aksjdhaksjlhslkdjfhlkjasd"
            }));
        }

        [Fact]
        public void TestCreateHash_ReturnHashSuccessfully()
        {
            var hash = _service.SpaceCoinFactory(12);

            Assert.Equal(512, Encoding.ASCII.GetByteCount(hash.Hash) * 8);
        }
    }
}