using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadRoomsQuery : IRequest<IEnumerable<OutputRoomDto>>
    {
        public class ReadRoomsQueryHandler : IRequestHandler<ReadRoomsQuery, IEnumerable<OutputRoomDto>>
        {
            private readonly ProductRepository _repository;
            private readonly UserAccessor _userAccessor;

            public ReadRoomsQueryHandler(UserAccessor userAccessor, ProductRepository repository)
            {
                _userAccessor = userAccessor;
                _repository = repository;
            }

            public async Task<IEnumerable<OutputRoomDto>> Handle(ReadRoomsQuery request,
                CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();

                var rooms = await _repository.ReadAllRoomsByLocationId(payload.LocationId);

                return rooms.Select(x => new OutputRoomDto
                {
                    RoomId = x.RoomId,
                    Category = "room",
                    ImageUri = x.Product.ImageUri,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    ProductId = x.ProductId,
                    Tags = x.Tags,
                    Capacity = x.Capacity
                });
            }
        }
    }
}