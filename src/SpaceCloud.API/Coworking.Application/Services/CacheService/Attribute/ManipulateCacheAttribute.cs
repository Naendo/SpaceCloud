using System;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Coworking.Application.Services
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ManipulateCacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly string _path;

        public ManipulateCacheAttribute(string path)
        {
            _path = path;
            if (!path.StartsWith("/")) _path = "/" + path;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userManager = context.HttpContext.RequestServices.GetService<UserAccessor>();
            var responseCacheService = context.HttpContext.RequestServices.GetService<IResponseCacheService>();

            if (!userManager.IsAuthenticated())
            {
                await next();
                return;
            }

            try
            {
                var payload = userManager.BuildPayload();
                await responseCacheService.ClearCacheAsync(
                    responseCacheService.CreateCacheKey(_path, payload.LocationId));
                await next();
            }
            catch (Exception)
            {
                await next();
            }
        }
    }
}