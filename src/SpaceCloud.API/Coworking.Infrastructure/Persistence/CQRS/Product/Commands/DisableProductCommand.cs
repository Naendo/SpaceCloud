using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class DisableProductCommand : IRequest
    {
        public DisableProductCommand(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }


        public class DisableProductCommandHandler : IRequestHandler<DisableProductCommand>
        {
            private readonly ProductRepository _repository;

            public DisableProductCommandHandler(ProductRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DisableProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repository.FindAsync(request.ProductId);

                if (entity is null)
                    throw new NotFoundException("product/disable", HttpMethod.Post,
                        "product-not-found");

                if (!entity.IsEnabled)
                    throw new BadRequestException("product/disable", HttpMethod.Post,
                        "product-is-disabled");

                entity.IsEnabled = false;
                await _repository.UpdateAsync(entity);
                return Unit.Value;
            }
        }
    }
}