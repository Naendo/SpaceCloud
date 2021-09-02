using System;

namespace Coworking.Application
{
    public class InputTicketDto
    {
        public DateTime CreationDate => DateTime.Now;
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}