using System;
using System.Linq;

namespace Coworking.Application.Services
{
    public class PasswordTokenFactory : ITokenGenerator
    {
        private const string CHAR_ARRAY = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        private const int TOKEN_LENGTH = 8;

        private readonly Random _rndGen = new Random();


        public (string Token, DateTime Expires) GenerateAndSetPasswordResetToken()
        {
            var token = new string(Enumerable.Repeat(CHAR_ARRAY, TOKEN_LENGTH)
                .Select(s => s[_rndGen.Next(s.Length)]).ToArray());

            var expires = DateTime.Now.AddMinutes(10);

            return (token, expires);
        }

        public string GenerateSchedulerAccessToken()
        {
            return new string(Enumerable.Repeat(CHAR_ARRAY, TOKEN_LENGTH).Select(s => s[_rndGen.Next(s.Length)])
                .ToArray());
        }
        
        public string GenerateOrderNr()
        {
            return new string(Enumerable.Repeat(CHAR_ARRAY + CHAR_ARRAY.ToLower() + "-/.", 12)
                .Select(x => x[_rndGen.Next(x.Length)])
                .ToArray());
        }
    }
}