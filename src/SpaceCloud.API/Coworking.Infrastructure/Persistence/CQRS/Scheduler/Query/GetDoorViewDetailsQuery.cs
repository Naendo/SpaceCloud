using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.ViewModels;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class GetDoorViewDetailsQuery : IRequest<OutputDoorViewDto>
    {
        public GetDoorViewDetailsQuery(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }

        public class GetRoomDetailsQueryHandler : IRequestHandler<GetDoorViewDetailsQuery, OutputDoorViewDto>
        {
            private readonly SchedulerRepository _repository;

            public GetRoomDetailsQueryHandler(SchedulerRepository repository)
            {
                _repository = repository;
            }


            public async Task<OutputDoorViewDto> Handle(GetDoorViewDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var result = await _repository.ReadSchedulerAsync(request.ProductId);

                if (result is null)
                    throw new NotFoundException($"room/view/{request.ProductId}", HttpMethod.Get, "no-orders-today");

                return new OutputDoorViewDto
                {
                    ProductId = result.ProductId,
                    RoomName = result.Product.Name,
                    Bookings = result.Product.CartEntries.Select(x => new OutputDoorViewTodaysBookingDto
                    {
                        EndDate = x.EndDate,
                        StartDate = x.StartDate,
                        FirstName = x.Product.LastModifiedBy.FirstName,
                        LastName = x.Product.LastModifiedBy.LastName,
                        OrderId = x.OrderId
                    }).ToList()
                };
            }
        }
    }
}