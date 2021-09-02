using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.ViewModels;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class GetOrderOfWorkerQuery : IRequest<IEnumerable<OutputGetOrderDto>>
    {
        public class
            GetOrderOfWorkerQueryHandler : IRequestHandler<GetOrderOfWorkerQuery, IEnumerable<OutputGetOrderDto>>
        {
            private readonly OrderRepository _repository;
            private readonly UserAccessor _userAccessor;

            public GetOrderOfWorkerQueryHandler(OrderRepository repository, UserAccessor userAccessor)
            {
                _repository = repository;
                _userAccessor = userAccessor;
            }

            public async Task<IEnumerable<OutputGetOrderDto>> Handle(GetOrderOfWorkerQuery request,
                CancellationToken cancellationToken)
            {
                var user = _userAccessor.BuildPayload();

                var order = await _repository.GetOrdersByUserId(user.UserId);

                return order.Select(x => new OutputGetOrderDto
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Created = x.CreatedAt,
                    OrderId = x.OrderId,
                    Price = x.GetPrice(),
                    Status = x.Status,
                    UserId = x.UserId
                });
            }
        }
    }
}