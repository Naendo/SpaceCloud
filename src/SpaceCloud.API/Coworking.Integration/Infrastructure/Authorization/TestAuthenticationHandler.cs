using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Coworking.Application.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Coworking.Integration.Tests
{
    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //AuthenticationUser: Pw: integration, mail: integration@test.com
            var claims = new List<Claim>
            {
                new Claim(nameof(JwtPayloadModel.UserId), "34"),
                new Claim(nameof(JwtPayloadModel.LocationId), "1"),
                new Claim(nameof(JwtPayloadModel.CompanyId), "1"),
                new Claim(ClaimTypes.Role, "Administrator")
            };


            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}