using System.Text.Json.Serialization;

namespace Coworking.Domain.Entities
{
    public class RoomTags : BaseEntity
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        [JsonIgnore] public int RoomId { get; set; }
        [JsonIgnore] public Room Room { get; set; }
    }
}