using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.ViewModels;
using Coworking.Infrastructure.Persistence;
using MediatR;

namespace Coworking.Infrastructure
{
    public class FetchCoworkingUsageQuery : IRequest<UsageEventDto>
    {
        public class FetchCoworkingUsageQueryHandler : IRequestHandler<FetchCoworkingUsageQuery, UsageEventDto>
        {
            private readonly CartRepository _cartRepository;
            private readonly ProductRepository _repository;
            private readonly UserAccessor _userAccessor;

            public FetchCoworkingUsageQueryHandler(UserAccessor userAccessor,
                ProductRepository repository,
                CartRepository cartRepository)
            {
                _userAccessor = userAccessor;
                _repository = repository;
                _cartRepository = cartRepository;
            }

            public async Task<UsageEventDto> Handle(FetchCoworkingUsageQuery request,
                CancellationToken cancellationToken)
            {
                var user = _userAccessor.BuildPayload();

                var desks = await _repository.ReadAllDesksByLocationId(user.LocationId);


                var currentDesks = 0;

                foreach (var desk in desks)
                {
                    var bookings = await _cartRepository.ReadAllCartsByProductId(desk.ProductId);
                    currentDesks = bookings.Count;
                }


                return new UsageEventDto
                {
                    MaxDesks = desks.Sum(x => x.ProductAmount),
                    CurrentDesks = currentDesks
                };
            }
        }
    }
}