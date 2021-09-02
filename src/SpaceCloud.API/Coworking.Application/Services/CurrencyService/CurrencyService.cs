using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Coworking.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Coworking.Application.Services
{
    public class CurrencyService
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly CurrencyOptions _options;

        public CurrencyService(IOptions<CurrencyOptions> options)
        {
            _options = options.Value;
        }

        private string CreateHash(SpaceCoin coin)
        {
            using var manager = new SHA512Managed();
            return string.Join("", manager.ComputeHash(CreateKey(coin.CoinGuid))
                .Select(x => x.ToString("X2")).ToArray());
        }

        public SpaceCoin SpaceCoinFactory(int userId)
        {
            var coin = new SpaceCoin
            {
                UserId = userId
            };

            coin.Hash = CreateHash(coin);
            return coin;
        }


        private byte[] CreateKey(Guid guid)
        {
            return _encoding.GetBytes($"SECRET{_options.CurrencySecret}ID{guid.ToString()})");
        }
    }
}