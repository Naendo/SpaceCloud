using Coworking.Domain.Enumerations;

namespace Coworking.Application
{
    public class SchedulerAuthorizeDto
    {
        public int UserId { get; set; }

        public int RoomId { get; set; }

        public int LocationId { get; set; }

        public UserType Role { get; set; }
    }
}