using System;
using System.Net.Http;
using System.Threading.Tasks;
using Coworking.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Coworking.Api.Filters
{
    public class CustomExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ILogger<CustomExceptionFilter> _logger;

        public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
        {
            _logger = logger;
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception is BadRequestException badRequestException)
            {
                context.Result = badRequestException.Result;
            }
            else if (context.Exception is NotFoundException notFoundException)
            {
                context.Result = notFoundException.Result;
            }
            else if (context.Exception is DuplicatedEntryException duplicatedEntryException)
            {
                context.Result = duplicatedEntryException.Result;
            }
            else if (context.Exception is InternalServerErrorException internalServerErrorException)
            {
                context.Result = internalServerErrorException.Result;
            }
            else if (context.Exception is ForbiddenException forbiddenException)
            {
                context.Result = forbiddenException.Result;
            }
            else
            {
                _logger.LogError($"[{DateTime.Now}]: {context.Exception.Message} {Environment.NewLine}" +
                                 $"{context.Exception.InnerException}");
                context.Result = new InternalServerErrorException($"{context.HttpContext.Request.Path}",
                        HttpMethod.Head,
                        $"An unhandled Exception has been thrown: {Environment.NewLine}{context.Exception}")
                    .Result;
            }

            return Task.CompletedTask;
        }
    }
}