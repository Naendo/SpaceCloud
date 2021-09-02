using System.Net;

namespace Coworking.Application.Services
{
    public class MailOptions
    {
        public const string POSITION = "MailOptions";


        public string ApiKey { get; set; }
        public string Mail { get; set; }
        public string Server { get; set; }
        public bool IsHtmlBody { get; set; } = true;
        public int Port { get; set; } = 465;
        public NetworkCredential Credentials { get; set; }
        public bool EnableSsl { get; set; } = true;
        public string RedirectUri { get; set; }
    }
}