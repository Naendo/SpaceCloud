using System;
using MediatR;

namespace Coworking.Application.ViewModels
{
    public class OutputReadSchedulerDto : IRequest<Unit>
    {
        public string Name { get; set; }

        public int RoomId { get; set; }

        public bool HasMeeting { get; set; }

        public SchedulerMeetingDto? Meetings { get; set; }
    }


    public class SchedulerMeetingDto
    {
        public SchedulerMeetingDto(SchedulerMeetingDto? meeting)
        {
            Meeting = meeting;
        }

        public string Usage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int RunTime => (int) (EndDate - StartDate).TotalSeconds;

        public int TimeToNextMeetingAfterEndDate
        {
            get
            {
                if (Meeting is null) return 0;
                return (int) (Meeting.StartDate - EndDate).TotalSeconds;
            }
        }

        public SchedulerMeetingDto? Meeting { get; }
    }
}