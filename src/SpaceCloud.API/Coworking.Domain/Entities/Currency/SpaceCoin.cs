using System;

namespace Coworking.Domain.Entities
{
    public class SpaceCoin : BaseEntity
    {
        public int CoinId { get; set; }

        public string Hash { get; set; }
        public Guid CoinGuid { get; } = Guid.NewGuid();

        public int UserId { get; set; }
        public User User { get; set; }
    }
}