using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class DeleteProductCommand : IRequest
    {
        public DeleteProductCommand(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }


        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
        {
            private readonly OrderRepository _orderRepository;
            private readonly ProductRepository _repository;

            public DeleteProductCommandHandler(ProductRepository repository, OrderRepository orderRepository)
            {
                _repository = repository;
                _orderRepository = orderRepository;
            }

            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var result = await _repository.ReadProductWithCartAsync(request.ProductId);


                if (result is null)
                    throw new NotFoundException("product/delete", HttpMethod.Delete, "product-not-found");
                var hasActive = HasActiveOrders(result);

                if (hasActive)
                    throw new BadRequestException("product/delete", HttpMethod.Delete, "product-has-active-order");

                await _repository.DeleteAsync(result);
                return Unit.Value;
            }

            private bool HasActiveOrders(Product product)
            {
                //return product.CartEntries
                //  .Any(x => x.StartDate > DateTime.Now || x.EndDate > DateTime.Now);
                return product.CartEntries.Count() != 0;
            }
        }
    }
}