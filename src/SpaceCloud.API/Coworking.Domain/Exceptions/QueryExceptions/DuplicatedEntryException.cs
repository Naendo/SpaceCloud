using System;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Domain.Exceptions
{
    [Serializable]
    public class DuplicatedEntryException : BaseHttpException<ObjectResult>
    {
        public DuplicatedEntryException(string path, HttpMethod method, string message) : base(new ObjectResult(
            new ExceptionModel
            {
                HttpMethod = method,
                Message = message,
                Path = path
            })
        {
            StatusCode = 409
        })
        {
        }
    }
}