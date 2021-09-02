using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Services;
using Coworking.Application.ViewModels.Product.Output;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadRoomDetailsQuery : IRequest<OutputRoomDetailsDto>
    {
        public ReadRoomDetailsQuery(int productId)
        {
            ProductId = productId;
        }

        public int ProductId { get; }


        public class ReadRoomDetailsQueryHandler : IRequestHandler<ReadRoomDetailsQuery, OutputRoomDetailsDto>
        {
            private readonly AvailabilityService _availabilityService;
            private readonly ProductRepository _repository;

            public ReadRoomDetailsQueryHandler(ProductRepository repository, AvailabilityService availabilityService)
            {
                _repository = repository;
                _availabilityService = availabilityService;
            }

            public async Task<OutputRoomDetailsDto> Handle(ReadRoomDetailsQuery request,
                CancellationToken cancellationToken)
            {
                var room = await _repository.ReadRoomByProductId(request.ProductId);

                if (room is null || room.Product is null)
                    throw new BadRequestException("product/room/get/{productId}", HttpMethod.Get,
                        "product-does-not-exist-or-is-not-a-room");


                return new OutputRoomDetailsDto
                {
                    Description = room.Product.Description,
                    ImageUri = room.Product.ImageUri,
                    Capacity = room.Capacity,
                    Name = room.Product.Name,
                    Price = room.Product.Price,
                    ProductId = room.ProductId,
                    RoomId = room.RoomId,
                    Tags = room.Tags,
                    Availability = _availabilityService.AvailabilityFactory(room.Product.CartEntries)
                };
            }
        }
    }
}