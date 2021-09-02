using System;

namespace Coworking.Application.ViewModels
{
    public class OutputDoorViewTodaysBookingDto
    {
        public int OrderId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}