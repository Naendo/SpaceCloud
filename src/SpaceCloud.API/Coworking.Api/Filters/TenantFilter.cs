using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;

namespace Coworking.Api.Filters
{
    public class TenantFilter : IAsyncActionFilter
    {
        private readonly IWebHostEnvironment _env;

        public TenantFilter(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("tenant", out StringValues tenantValue))
            {
                context.HttpContext.User.Identities.FirstOrDefault()
                    .AddClaim(new Claim(nameof(UserModel.Tenant), tenantValue.ToString()));
                await next();
            }
            else if (context.HttpContext.Request.Headers.TryGetValue("Origin", out StringValues originStringValue))
            {
                var origin = originStringValue.ToString();

                if (_env.IsDevelopment() || origin.Contains("localhost:5001"))
                    origin = origin.Replace("localhost:5001", "dev.localhost:5001");

                var httpsLenght = "https://".Length;

                var tenant = origin.Remove(0, httpsLenght)[..(origin.IndexOf('.') - httpsLenght)];

                context.HttpContext.User.Identities.FirstOrDefault()
                    .AddClaim(new Claim(nameof(UserModel.Tenant), tenant));
                await next();
            }
            else
            {
                if (_env.IsDevelopment())
                {
                    context.HttpContext.User.Identities.FirstOrDefault()
                        .AddClaim(new Claim(nameof(UserModel.Tenant), "dev"));
                    await next();
                }
                else
                    throw new BadRequestException("tenant", new HttpMethod(context.HttpContext.Request.Method),
                        "no-tenant");
            }
        }
    }
}