using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Domain.Exceptions
{
    [Serializable]
    public class BadRequestException : BaseHttpException<BadRequestObjectResult>
    {
        public BadRequestException(string handlerName, HttpMethod method, string message)
            : base(new BadRequestObjectResult(
                new ExceptionModel {HttpMethod = method, Message = message, Path = handlerName}))
        {
        }
    }
}