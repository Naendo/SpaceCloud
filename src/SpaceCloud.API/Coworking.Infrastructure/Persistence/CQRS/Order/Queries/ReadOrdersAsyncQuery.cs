using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.ViewModels;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadOrdersAsyncQuery : IRequest<IEnumerable<OutputGetOrderDto>>
    {
        public class GetOrdersQueryHandler : IRequestHandler<ReadOrdersAsyncQuery, IEnumerable<OutputGetOrderDto>>
        {
            private readonly OrderRepository _repository;
            private readonly UserAccessor _userAccessor;

            public GetOrdersQueryHandler(OrderRepository repository, UserAccessor userAccessor)
            {
                _repository = repository;
                _userAccessor = userAccessor;
            }

            public async Task<IEnumerable<OutputGetOrderDto>> Handle(ReadOrdersAsyncQuery request,
                CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();

                var result = await _repository.GetAllOrdersByLocationId(payload.LocationId);


                return result.Select(x => new OutputGetOrderDto
                {
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    OrderId = x.OrderId,
                    Price = x.CartItems.Sum(y => y.Product.Price),
                    UserId = x.UserId,
                    Status = x.Status,
                    Created = x.CreatedAt
                });
            }
        }
    }
}