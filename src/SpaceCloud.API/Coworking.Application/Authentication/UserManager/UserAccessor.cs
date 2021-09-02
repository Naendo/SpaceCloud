using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Coworking.Application.Authentication.UserManager
{
    public class UserAccessor
    {
        private readonly HttpContext _httpContext;

        public UserAccessor(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext;
        }


        public bool IsAuthenticated()
        {
            return _httpContext.User.Identity.IsAuthenticated;
        }

        public UserModel BuildPayload()
        {
            var user = _httpContext.User;
            if (!_httpContext.User.Identity.IsAuthenticated)
            {
                var claim = user.Claims.FirstOrDefault(x => x.Type == nameof(UserModel.Tenant));
                return new UserModel
                {
                    Tenant = claim.Value
                };
            }


            return new UserModel
            {
                UserId = int.Parse(user.Claims.FirstOrDefault(x => x.Type == nameof(JwtPayloadModel.UserId))!.Value),
                CompanyId = int.Parse(
                    user.Claims.FirstOrDefault(x => x.Type == nameof(JwtPayloadModel.CompanyId))!.Value),
                Role = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value,
                LocationId = int.Parse(user.Claims.FirstOrDefault(x => x.Type == nameof(JwtPayloadModel.LocationId))!
                    .Value),
                Tenant = user.Claims.FirstOrDefault(x => x.Type == "Tenant")!.Value
            };
        }
    }
}