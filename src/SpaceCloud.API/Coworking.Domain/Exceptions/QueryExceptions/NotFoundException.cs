using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Domain.Exceptions
{
    [Serializable]
    public class NotFoundException : BaseHttpException<NotFoundObjectResult>
    {
        public NotFoundException(string path, HttpMethod method, string message) : base(new NotFoundObjectResult(
            new ExceptionModel
                {HttpMethod = method, Message = message, Path = path}))
        {
        }
    }
}