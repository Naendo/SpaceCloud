using System;
using Coworking.Domain.Enumerations;

namespace Coworking.Domain.Entities
{
    public class Identity : BaseEntity
    {
        public int IdentityId { get; set; }
        public DateTime LastLogged { get; set; } = DateTime.Now;
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Mail { get; set; }
        public UserType Role { get; set; }

        public bool StayLogged { get; set; } = false;

        public string? PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenExpires { get; set; }

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpires { get; set; }


        public User User { get; set; }
    }
}