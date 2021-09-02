using System;
using Coworking.Domain.Entities;

namespace Coworking.Domain
{
    public class Scheduler : BaseEntity
    {
        public int SchedulerId { get; set; }

        public DateTime? LastLogged { get; set; }

        public string ActivatorToken { get; set; }

        public string? ReferenceToken { get; set; }

        public DateTime? TokenExpires { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public User User { get; set; }

        public Room Room { get; set; }
    }
}