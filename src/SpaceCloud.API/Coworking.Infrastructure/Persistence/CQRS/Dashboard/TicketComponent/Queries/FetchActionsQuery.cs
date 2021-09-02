using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.ViewModels;
using MediatR;

namespace Coworking.Infrastructure
{
    public class FetchActionsQuery : IRequest<IEnumerable<RecentEventDto>>
    {
        public class FetchActionsQueryHandler : IRequestHandler<FetchActionsQuery, IEnumerable<RecentEventDto>>
        {
            private readonly RecentActionRepository _repository;
            private readonly UserAccessor _userAccessor;


            public FetchActionsQueryHandler(UserAccessor userAccessor, RecentActionRepository repository)
            {
                _userAccessor = userAccessor;
                _repository = repository;
            }

            public async Task<IEnumerable<RecentEventDto>> Handle(FetchActionsQuery request,
                CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();
                var result = await _repository.GetRecentActionWithLocationId(payload.LocationId);

                return result.Select(x => new RecentEventDto
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Action = x.Action,
                    Date = x.CreatedAt,
                    UserId = x.UserId
                });
            }
        }
    }
}