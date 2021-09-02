using System.Collections.Generic;

namespace Coworking.Domain.Entities
{
    public class CompanyLocation : BaseEntity
    {
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public string? Door { get; set; }

        public string? Floor { get; set; }

        public int CompanyId { get; set; }
        public virtual Company Company { get; set; }

        public List<User> Users { get; set; }
        public List<Product> Products { get; set; }
    }
}