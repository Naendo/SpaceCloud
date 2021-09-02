using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.Services;
using Coworking.Domain.Entities;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class AddRoomCommand : IRequest
    {
        public string Description { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public List<string> Tags { get; set; }

        public class AddRoomCommandHandler : IRequestHandler<AddRoomCommand>
        {
            private readonly ProductRepository _repository;
            private readonly UserAccessor _userAccessor;
            private readonly TeamupService _teamupService;

            public AddRoomCommandHandler(ProductRepository repository, UserAccessor userAccessor,
                TeamupService teamupService)
            {
                _repository = repository;
                _userAccessor = userAccessor;
                _teamupService = teamupService;
            }

            public async Task<Unit> Handle(AddRoomCommand request, CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();

                if (payload.Tenant == "dev")
                {
                    var result = await _teamupService.AddRoomToCalendarAsync(request.Name);

                    if (!result.IsSuccessStatusCode)
                        throw new BadRequestException(nameof(_teamupService.AddRoomToCalendarAsync), HttpMethod.Post,
                            "teamup-service-failed-at-post-async");
                }

                var room = new Room
                {
                    Capacity = request.Capacity,
                    Product = new Product
                    {
                        Name = request.Name,
                        Description = request.Description,
                        ImageUri = request.ImageUri,
                        IsEnabled = true,
                        Price = request.Price,
                        LastModifiedByUserId = payload.UserId,
                        LocationId = payload.LocationId
                    },
                    Tags = request.Tags.Select(x => new RoomTags
                    {
                        Name = x.Normalize()
                    }).ToList()
                };

                await _repository.AddRoomAsync(room);


                return Unit.Value;
            }
        }
    }
}