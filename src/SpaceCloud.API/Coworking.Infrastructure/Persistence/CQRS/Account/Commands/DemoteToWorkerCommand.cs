using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Domain.Enumerations;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class DemoteToWorkerCommand : IRequest<PermissionDto>
    {
        public DemoteToWorkerCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }


        public class DemoteToWorkerCommandHandler : IRequestHandler<DemoteToWorkerCommand, PermissionDto>
        {
            private readonly UserRepository _repository;


            public DemoteToWorkerCommandHandler(UserRepository repository)
            {
                _repository = repository;
            }

            public async Task<PermissionDto> Handle(DemoteToWorkerCommand request, CancellationToken cancellationToken)
            {
                var identity = await _repository.ReadUserById(request.UserId);

                if (identity.Identity.Role == UserType.Worker)
                    throw new BadRequestException("account/demote", HttpMethod.Put, "cannot-demote-a-worker");

                if (identity.Identity.Role == UserType.Owner)
                    throw new BadRequestException("account/demote", HttpMethod.Put, "cannot-demote-owner");


                identity.Identity.Role = UserType.Worker;
                await _repository.UpdateAsync(identity);
                return new PermissionDto
                {
                    Role = identity.Identity.Role.ToString(),
                    UserId = identity.UserId
                };
            }
        }
    }
}