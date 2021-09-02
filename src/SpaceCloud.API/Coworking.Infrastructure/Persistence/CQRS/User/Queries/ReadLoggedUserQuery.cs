using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using MediatR;

namespace Coworking.Infrastructure
{
    public class ReadLoggedUserQuery : IRequest<OutputUserDto>
    {
        public class ReadLoggedUserQueryHandler : IRequestHandler<ReadLoggedUserQuery, OutputUserDto>
        {
            private readonly UserAccessor _userAccessor;
            private readonly UserRepository _userRepository;

            public ReadLoggedUserQueryHandler(UserRepository userRepository, UserAccessor userAccessor)
            {
                _userRepository = userRepository;
                _userAccessor = userAccessor;
            }

            public async Task<OutputUserDto> Handle(ReadLoggedUserQuery request, CancellationToken cancellationToken)
            {
                var user = _userAccessor.BuildPayload();
                var result = await _userRepository.ReadUserById(user.UserId);

                return new OutputUserDto
                {
                    FirstName = result.FirstName,
                    ImageUri = result.ImageUri!,
                    LastName = result.LastName,
                    Mail = result.Identity.Mail,
                    Role = result.Identity.Role.ToString(),
                    UserId = result.UserId
                };
            }
        }
    }
}