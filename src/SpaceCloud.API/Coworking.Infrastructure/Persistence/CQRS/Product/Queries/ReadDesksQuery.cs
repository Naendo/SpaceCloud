using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadDesksQuery : IRequest<IEnumerable<OutputDeskDto>>
    {
        public class ReadDesksQueryHandler : IRequestHandler<ReadDesksQuery, IEnumerable<OutputDeskDto>>
        {
            private readonly ProductRepository _repository;
            private readonly UserAccessor _userAccessor;

            public ReadDesksQueryHandler(UserAccessor userAccessor, ProductRepository repository)
            {
                _userAccessor = userAccessor;
                _repository = repository;
            }

            public async Task<IEnumerable<OutputDeskDto>> Handle(ReadDesksQuery request,
                CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();

                var desks = await _repository.ReadAllDesksByLocationId(payload.LocationId);

                return desks.Select(x => new OutputDeskDto
                {
                    Category = "desk",
                    ImageUri = x.Product.ImageUri,
                    IntervalKey = x.IntervalType,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    ProductId = x.ProductId
                });
            }
        }
    }
}