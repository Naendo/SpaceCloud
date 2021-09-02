using System;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Domain.Exceptions
{
    [Serializable]
    public class ForbiddenException : BaseHttpException<ForbidResult>
    {
        public ForbiddenException() : base(new ForbidResult())
        {
        }
    }
}