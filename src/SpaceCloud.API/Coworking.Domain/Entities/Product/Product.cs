using System.Collections.Generic;

namespace Coworking.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public bool IsEnabled { get; set; } = true;
        public decimal Price { get; set; }

        public int LastModifiedByUserId { get; set; }
        public virtual User LastModifiedBy { get; set; }

        public int LocationId { get; set; }
        public CompanyLocation Location { get; set; }


        public Desk? Desk { get; set; }
        public Room? Room { get; set; }
        public List<Cart> CartEntries { get; set; }
    }
}