using System;

namespace Coworking.Application.ViewModels
{
    public class RecentEventDto
    {
        public int UserId { get; set; }
        public string Action { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime Date { get; set; }
    }
}