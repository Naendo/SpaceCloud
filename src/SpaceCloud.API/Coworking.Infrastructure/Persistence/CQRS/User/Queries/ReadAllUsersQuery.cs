using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadAllUsersQuery : IRequest<List<OutputUserDto>>
    {
        public class RealAllUsersQueryHandler : IRequestHandler<ReadAllUsersQuery, List<OutputUserDto>>
        {
            private readonly UserRepository _repository;
            private readonly UserAccessor _userAccessor;

            public RealAllUsersQueryHandler(UserRepository repository, UserAccessor userAccessor)
            {
                _repository = repository;
                _userAccessor = userAccessor;
            }

            public async Task<List<OutputUserDto>> Handle(ReadAllUsersQuery request,
                CancellationToken cancellationToken)
            {
                var locationId = _userAccessor.BuildPayload().LocationId;


                var result = await _repository.ReadUsersWithLocationId(locationId);
                if (result is null)
                    throw new NotFoundException("user/get", HttpMethod.Get, "location-id-not-found");

                return result.Select(x => new OutputUserDto
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ImageUri = x.ImageUri!,
                    Mail = x.Identity.Mail,
                    Role = x.Identity.Role.ToString(),
                    UserId = x.UserId
                }).ToList();
            }
        }
    }
}