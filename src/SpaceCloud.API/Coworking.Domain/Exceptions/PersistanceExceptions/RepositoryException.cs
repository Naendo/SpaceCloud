using System;
using System.Runtime.Serialization;

namespace Coworking.Domain
{
    [Serializable]
    public class RepositoryException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public RepositoryException()
        {
        }

        public RepositoryException(string message) : base(message)
        {
        }

        public RepositoryException(string message, Exception inner) : base(message, inner)
        {
        }

        protected RepositoryException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}