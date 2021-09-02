using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Coworking.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Coworking.Application.Authentication
{
    public class TokenService : ITokenService
    {
        private const string REFRESH_TOKEN_KEY = "refresh_token";
        private readonly HttpContext _context;

        private readonly Encoding _encoding = Encoding.ASCII;

        private readonly JwtOptions _options;

        public TokenService(IOptions<JwtOptions> options, IHttpContextAccessor httpContext)
        {
            _options = options.Value;
            _context = httpContext.HttpContext;
        }

        public OutputAuthorizeDto CreateToken(JwtPayloadModel payloadModel)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(nameof(payloadModel.UserId), payloadModel.UserId.ToString()),
                    new Claim(nameof(payloadModel.LocationId), payloadModel.LocationId.ToString()),
                    new Claim(nameof(payloadModel.CompanyId), payloadModel.CompanyId.ToString()),
                    new Claim(ClaimTypes.Role, payloadModel.Role)
                };

                var key = new SymmetricSecurityKey(_encoding.GetBytes(_options.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(2).Add(_options.TokenLifeTime),
                    SigningCredentials = credentials
                };

                var handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(descriptor);


                return new OutputAuthorizeDto
                {
                    Exp = descriptor.Expires.Value,
                    Token = handler.WriteToken(token)
                };
            }
            catch (SecurityTokenEncryptionFailedException)
            {
                throw new BadRequestException(nameof(CreateToken), HttpMethod.Post, "security-key-to-short");
            }
        }

        public OutputAuthorizeDto CreateReadOnlyToken(SchedulerAuthorizeDto payloadModel)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(nameof(payloadModel.UserId), payloadModel.UserId.ToString()),
                    new Claim(nameof(payloadModel.LocationId), payloadModel.LocationId.ToString()),
                    new Claim(ClaimTypes.Role, "Scheduler"),
                    new Claim(nameof(payloadModel.RoomId), payloadModel.RoomId.ToString())
                };

                var key = new SymmetricSecurityKey(_encoding.GetBytes(_options.Secret));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                var descriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = credentials
                };

                var handler = new JwtSecurityTokenHandler();
                var token = handler.CreateToken(descriptor);


                return new OutputAuthorizeDto
                {
                    Token = handler.WriteToken(token),
                    Exp = DateTime.Now.AddDays(10)
                };
            }
            catch (SecurityTokenEncryptionFailedException)
            {
                throw new BadRequestException(nameof(CreateToken), HttpMethod.Post, "security-key-to-short");
            }
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}