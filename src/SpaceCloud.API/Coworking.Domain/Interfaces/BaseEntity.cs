using System;

namespace Coworking.Domain
{
    public class BaseEntity
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}