using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.ViewModels.Product.Output;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadDeskDetailsQuery : IRequest<OutputDeskDetailsDto>
    {
        public ReadDeskDetailsQuery(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }

        public class ReadDeskDetailsQueryHandler : IRequestHandler<ReadDeskDetailsQuery, OutputDeskDetailsDto>
        {
            private readonly ProductRepository _repository;

            public ReadDeskDetailsQueryHandler(ProductRepository repository)
            {
                _repository = repository;
            }

            public async Task<OutputDeskDetailsDto> Handle(ReadDeskDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var desk = await _repository.ReadDeskByProductId(request.ProductId);

                return new OutputDeskDetailsDto
                {
                    Description = desk.Product.Description,
                    ProductId = desk.ProductId,
                    DeskId = desk.DeskId,
                    ImageUri = desk.Product.ImageUri,
                    IntervalType = desk.IntervalType,
                    Name = desk.Product.Name,
                    Price = desk.Product.Price,
                    CurrentlyAvailable = desk.ProductAmount >= 1,
                    AvailableAmount = desk.ProductAmount
                };
            }
        }
    }
}