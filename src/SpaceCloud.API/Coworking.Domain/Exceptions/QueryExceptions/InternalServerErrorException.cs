using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Domain.Exceptions
{
    [Serializable]
    public class InternalServerErrorException : BaseHttpException<ObjectResult>
    {
        public InternalServerErrorException(string path, HttpMethod method, string message) : base(
            new ObjectResult(new ExceptionModel {HttpMethod = method, Message = message, Path = path})
                {StatusCode = 500})
        {
        }
    }
}