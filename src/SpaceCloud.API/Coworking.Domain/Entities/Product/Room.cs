using System.Collections.Generic;

namespace Coworking.Domain.Entities
{
    public class Room : BaseEntity
    {
        public int RoomId { get; set; }
        public int Capacity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = new Product();

        public Scheduler? Scheduler { get; set; }
        public List<RoomTags> Tags { get; set; }
    }
}