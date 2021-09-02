using System;
using System.Text.Json;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Coworking.Application.Services
{
    public class CacheableAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _cacheLifeTime;

        public CacheableAttribute(int cacheLifeTime = 3600)
        {
            _cacheLifeTime = cacheLifeTime;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetService<IResponseCacheService>();
            var tokenService = context.HttpContext.RequestServices.GetService<UserAccessor>();

            var payload = tokenService.BuildPayload();
            var cacheKey = cacheService.CreateCacheKey(context.HttpContext.Request.Path, payload.LocationId);

            context.HttpContext.Response.GetTypedHeaders().CacheControl =
                new CacheControlHeaderValue
                {
                    Public = true,
                    MaxAge = TimeSpan.FromSeconds(_cacheLifeTime),
                    NoCache = true
                };


            var result = await cacheService.CacheResponseAsync(cacheKey);
            if (result is null)
            {
                var executedActionContext = await next();

                if (executedActionContext.Result is null || executedActionContext.Result is EmptyResult) return;


                var content = JsonConvert.SerializeObject(((ObjectResult) executedActionContext.Result).Value,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new CamelCaseNamingStrategy()
                        }
                    });

                //Add to Cache
                await cacheService.SetCacheValueAsync(cacheKey, content, _cacheLifeTime);
                return;
            }

            //
            context.Result = new ObjectResult(result)
            {
                ContentTypes = new MediaTypeCollection
                {
                    "application/json",
                    "test/json",
                    "text/plain"
                },
                Formatters = new FormatterCollection<IOutputFormatter>
                {
                    new SystemTextJsonOutputFormatter(new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    })
                },
                StatusCode = 200
            };
        }
    }
}