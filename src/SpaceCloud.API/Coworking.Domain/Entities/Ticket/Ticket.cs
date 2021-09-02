namespace Coworking.Domain.Entities
{
    public class Ticket : BaseEntity
    {
        public int TicketId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}