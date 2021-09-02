using System;

namespace Coworking.Application.Authentication
{
    public class JwtOptions
    {
        public static string Position = nameof(JwtOptions);

        public string Secret { get; set; }
        public TimeSpan TokenLifeTime { get; set; }
    }
}