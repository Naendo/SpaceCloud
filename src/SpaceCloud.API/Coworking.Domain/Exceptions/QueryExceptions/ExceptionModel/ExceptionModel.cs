using System.Net.Http;

namespace Coworking.Domain.Exceptions
{
    public class ExceptionModel
    {
        public string Path { get; set; }
        public string Message { get; set; }
        public HttpMethod HttpMethod { get; set; }
    }
}