using System.Text.Json.Serialization;

namespace Coworking.Domain.Entities
{
    public class RecentEvent : BaseEntity
    {
        public int ActionId { get; set; }
        public string Action { get; set; }

        public int UserId { get; set; }

        [JsonIgnore] public User User { get; set; }
    }
}