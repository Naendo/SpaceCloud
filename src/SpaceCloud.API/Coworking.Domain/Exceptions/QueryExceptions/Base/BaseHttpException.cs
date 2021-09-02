using System;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Domain.Exceptions
{
    [Serializable]
    public abstract class BaseHttpException<TResult> : Exception where TResult : IActionResult
    {
        protected BaseHttpException(TResult result)
        {
            Result = result;
        }

        public TResult Result { get; }
    }
}