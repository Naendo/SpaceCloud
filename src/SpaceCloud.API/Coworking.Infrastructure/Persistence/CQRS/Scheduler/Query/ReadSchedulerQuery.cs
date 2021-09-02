using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.ViewModels;
using Coworking.Domain.Entities;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class ReadSchedulerQuery : IRequest<OutputReadSchedulerDto>
    {
        public ReadSchedulerQuery(string token)
        {
            Token = token;
        }

        public string Token { get; }

        public class ReadSchedulerQueryHandler : IRequestHandler<ReadSchedulerQuery, OutputReadSchedulerDto>
        {
            private readonly SchedulerRepository _repository;

            public ReadSchedulerQueryHandler(SchedulerRepository repository)
            {
                _repository = repository;
            }


            public async Task<OutputReadSchedulerDto> Handle(ReadSchedulerQuery request,
                CancellationToken cancellationToken)
            {
                var result = await _repository.FindSchedulerByActivator(request.Token);


                var hasMeeting = result.Room.Product.CartEntries
                    .Any(x => x.StartDate > DateTime.Now
                              && x.EndDate < DateTime.Now);


                var meetings = await _repository.ReadAllCartEntriesForCurrentDateByScheduler(result);


                SchedulerMeetingDto MeetingFactory(Cart cart, SchedulerMeetingDto? previousMeeting)
                {
                    return new SchedulerMeetingDto(previousMeeting)
                    {
                        EndDate = cart.EndDate,
                        StartDate = cart.StartDate,
                        Usage = cart.Usage
                    };
                }

                SchedulerMeetingDto? meeting = null;

                for (var j = meetings.Count - 1; j >= 0; j--) meeting = MeetingFactory(meetings[j], meeting);


                return new OutputReadSchedulerDto
                {
                    HasMeeting = hasMeeting,
                    Name = result.Room.Product.Name,
                    RoomId = result.RoomId,
                    Meetings = meeting
                };
            }
        }
    }
}