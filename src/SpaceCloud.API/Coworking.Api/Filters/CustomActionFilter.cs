using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Coworking.Api.Filters
{
    public class CustomActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();

            var identity = context.HttpContext.User.Identities.FirstOrDefault(x => x.Label == "DefaultIdentity");

            if (identity is null) return;
            if (!identity.IsAuthenticated) return;


            var statusCode = context.HttpContext.Request.Method.ToLower() switch
            {
                "post" => 201,
                "get" => 200,
                "delete" => 204,
                "put" => 200,
                _ => throw new NotImplementedException()
            };

            context.Result = new ObjectResult(context.Result) {StatusCode = statusCode};
        }
    }
}