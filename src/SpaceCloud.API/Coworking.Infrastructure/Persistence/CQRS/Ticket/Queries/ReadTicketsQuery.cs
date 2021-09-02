using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class ReadTicketsQuery : IRequest<List<OutputTicketDto>>
    {
        public class ReadTicketsQueryHandler : IRequestHandler<ReadTicketsQuery, List<OutputTicketDto>>
        {
            private readonly TicketRepository _repository;
            private readonly UserAccessor _userAccessor;

            public ReadTicketsQueryHandler(TicketRepository repository, UserAccessor userAccessor)
            {
                _userAccessor = userAccessor;
                _repository = repository;
            }

            public async Task<List<OutputTicketDto>> Handle(ReadTicketsQuery request,
                CancellationToken cancellationToken)
            {
                var locationId = _userAccessor.BuildPayload().LocationId;

                var result = await _repository.GetAllTicketsIncludeUserAsync(locationId);

                return result.Select(x => new OutputTicketDto
                {
                    Content = x.Content,
                    Subject = x.Subject,
                    UserId = x.UserId,
                    CreationDate = x.CreatedAt,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    TicketId = x.TicketId
                }).ToList();
            }
        }
    }
}