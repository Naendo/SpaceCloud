using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class EnableProductCommand : IRequest
    {
        public EnableProductCommand(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }


        public class EnableProductCommandHandler : IRequestHandler<EnableProductCommand>
        {
            private readonly ProductRepository _repository;

            public EnableProductCommandHandler(ProductRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(EnableProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _repository.FindAsync(request.ProductId);

                if (entity is null)
                    throw new NotFoundException("product/disable", HttpMethod.Post,
                        "product-not-found");

                if (entity.IsEnabled)
                    throw new BadRequestException("product/disable", HttpMethod.Post,
                        "product-is-enabled");

                entity.IsEnabled = true;
                await _repository.UpdateAsync(entity);
                return Unit.Value;
            }
        }
    }
}