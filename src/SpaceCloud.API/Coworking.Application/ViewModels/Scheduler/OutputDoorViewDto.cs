using System.Collections.Generic;

namespace Coworking.Application.ViewModels
{
    public class OutputDoorViewDto
    {
        public int ProductId { get; set; }

        public string RoomName { get; set; }

        public List<OutputDoorViewTodaysBookingDto>? Bookings { get; set; }
    }
}