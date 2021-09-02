using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.ViewModels;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class DeclineOrderCommand : IRequest<OutputGetOrderDto>
    {
        public DeclineOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }

        public class DeclineOrderCommandHandler : IRequestHandler<DeclineOrderCommand, OutputGetOrderDto>
        {
            private readonly OrderRepository _repository;

            public DeclineOrderCommandHandler(OrderRepository repository)
            {
                _repository = repository;
            }

            public async Task<OutputGetOrderDto> Handle(DeclineOrderCommand request,
                CancellationToken cancellationToken)
            {
                var order = await _repository.FindOrderIncludeUserAsync(request.OrderId);

                if (order is null)
                    throw new NotFoundException("order/decline", HttpMethod.Post, "order-not-found");

                if (order.IsAccepted)
                    throw new BadRequestException("order/decline", HttpMethod.Post, "order-already-accepted");
                if (order.IsDeclined)
                    throw new BadRequestException("order/decline", HttpMethod.Post, "order-already-declined");


                order.IsDeclined = true;
                await _repository.UpdateAsync(order);


                return new OutputGetOrderDto
                {
                    FirstName = order.User.FirstName,
                    LastName = order.User.LastName,
                    Price = order.GetPrice(),
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    Status = order.Status
                };
            }
        }
    }
}