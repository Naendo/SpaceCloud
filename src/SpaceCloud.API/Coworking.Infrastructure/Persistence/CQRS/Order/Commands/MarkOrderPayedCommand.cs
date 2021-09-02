using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.ViewModels;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class MarkOrderPayedCommand : IRequest<OutputGetOrderDto>
    {
        public MarkOrderPayedCommand(int orderId)
        {
            OrderId = orderId;
        }

        private int OrderId { get; }

        public class MarkOrderPayedCommandHandler : IRequestHandler<MarkOrderPayedCommand, OutputGetOrderDto>
        {
            private readonly OrderRepository _repository;

            public MarkOrderPayedCommandHandler(OrderRepository repository)
            {
                _repository = repository;
            }


            public async Task<OutputGetOrderDto> Handle(MarkOrderPayedCommand request,
                CancellationToken cancellationToken)
            {
                var order = await _repository.FindOrderIncludeUserAsync(request.OrderId);

                if (order is null)
                    throw new NotFoundException("order/accept", HttpMethod.Post, "order-id-not-found");

                if (order.IsPayed)
                    throw new BadRequestException("order/accept", HttpMethod.Post, "order-already-payed");
                if (order.IsDeclined)
                    throw new BadRequestException("order/accept", HttpMethod.Post, "order-already-declined");


                order.IsPayed = true;

                await _repository.UpdateAsync(order);

                return new OutputGetOrderDto
                {
                    Created = order.CreatedAt,
                    FirstName = order.User.FirstName,
                    LastName = order.User.LastName,
                    OrderId = order.OrderId,
                    Price = order.GetPrice(),
                    Status = order.Status,
                    UserId = order.UserId
                };
            }
        }
    }
}