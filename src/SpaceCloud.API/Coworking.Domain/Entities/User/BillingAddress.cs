namespace Coworking.Domain.Entities
{
    public class BillingAddress : BaseEntity
    {
        public int BillingId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string? Floor { get; set; }
        public string? Door { get; set; }

        public User User { get; set; }
    }
}