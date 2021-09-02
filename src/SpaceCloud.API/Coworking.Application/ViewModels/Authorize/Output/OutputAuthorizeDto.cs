using System;

namespace Coworking.Application
{
    public class OutputAuthorizeDto
    {
        public string Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshExp { get; set; }
        public DateTime Exp { get; set; }
    }
}