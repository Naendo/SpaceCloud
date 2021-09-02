using System.Collections.Generic;

namespace Coworking.Domain.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageUri { get; set; }


        public int? BillingId { get; set; }
        public int IdentityId { get; set; }
        public int LocationId { get; set; }


        public CompanyLocation Location { get; set; }
        public Identity Identity { get; set; }
        public BillingAddress BillingAddress { get; set; }


        public List<Ticket> Tickets { get; set; }
        public List<RecentEvent> Events { get; set; }
        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        public List<Scheduler> Schedulers { get; set; }
        public ICollection<SpaceCoin> SpaceCoins { get; set; } = new HashSet<SpaceCoin>();
    }
}