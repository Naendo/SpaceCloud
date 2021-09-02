using System;

namespace Coworking.Application.Services
{
    public class HashModel
    {
        public string Password { get; set; }
        public Guid Salt { get; set; }
        public string SaltWithStringFormat => Salt.ToString("D");
    }
}