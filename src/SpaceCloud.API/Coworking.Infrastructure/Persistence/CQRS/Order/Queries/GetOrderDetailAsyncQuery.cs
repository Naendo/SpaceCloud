using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.ViewModels;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class GetOrderDetailAsyncQuery : IRequest<OutputGetDetailedOrderDto>
    {
        public GetOrderDetailAsyncQuery(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }

        public class
            GetOrderDetailAsyncQueryHandler : IRequestHandler<GetOrderDetailAsyncQuery, OutputGetDetailedOrderDto>
        {
            private readonly OrderRepository _repository;


            public GetOrderDetailAsyncQueryHandler(OrderRepository repository)
            {
                _repository = repository;
            }

            public async Task<OutputGetDetailedOrderDto> Handle(GetOrderDetailAsyncQuery request,
                CancellationToken cancellationToken)
            {
                var result = await _repository.FindOrderIncludeUserAsync(request.OrderId);

                if (result is null)
                    throw new NotFoundException("order/get/id", HttpMethod.Get, "order-not-found");


                var pdfUri = result.Invoice is null ? "" : result.Invoice.PdfUri;

                return new OutputGetDetailedOrderDto
                {
                    FirstName = result.User.FirstName,
                    LastName = result.User.LastName,
                    OrderId = result.OrderId,
                    Price = result.GetPrice(),
                    UserId = result.UserId,
                    Status = result.Status,
                    PdfUri = pdfUri,
                    Items = result.CartItems.Select(x => new OutputGetDetailedCartItemDto
                    {
                        EndDate = x.EndDate,
                        StartDate = x.StartDate,
                        ImageUri = x.Product.ImageUri,
                        Price = x.Product.Price,
                        ProductId = x.ProductId,
                        ProductName = x.Product.Name,
                        Usage = x.Usage
                    })
                };
            }
        }
    }
}