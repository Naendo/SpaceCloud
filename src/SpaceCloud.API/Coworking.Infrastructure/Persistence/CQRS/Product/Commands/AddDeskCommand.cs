using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Domain.Entities;
using Coworking.Domain.Enumerations;
using MediatR;

namespace Coworking.Infrastructure
{
    public class AddDeskCommand : IRequest
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public int StockAmount { get; set; }
        public DeskIntervalType IntervalType { get; set; }

        public class AddDeskCommandHandler : IRequestHandler<AddDeskCommand>
        {
            private readonly ProductRepository _repository;
            private readonly UserAccessor _userAccessor;

            public AddDeskCommandHandler(ProductRepository repository, UserAccessor userAccessor)
            {
                _repository = repository;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(AddDeskCommand request, CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();


                var desk = new Desk
                {
                    ProductAmount = request.StockAmount,
                    IntervalType = request.IntervalType,

                    Product = new Product
                    {
                        Name = request.Name,
                        Description = request.Description,
                        ImageUri = request.ImageUri,
                        IsEnabled = true,
                        Price = request.Price,
                        LastModifiedByUserId = payload.UserId,
                        LocationId = payload.LocationId
                    }
                };

                await _repository.AddDeskAsync(desk);
                return Unit.Value;
            }
        }
    }
}