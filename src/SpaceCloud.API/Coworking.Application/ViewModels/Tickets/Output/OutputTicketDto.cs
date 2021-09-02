using System;

namespace Coworking.Application
{
    public class OutputTicketDto
    {
        public int TicketId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UserId { get; set; }
    }
}