using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Interfaces;
using Coworking.Domain.Entities;
using Coworking.Domain.Enumerations;
using Coworking.Domain.Exceptions;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class PromoteToAdminCommand : IRequest<PermissionDto>
    {
        public PromoteToAdminCommand(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; }

        public class PromoteToAdminCommandHandler : IRequestHandler<PromoteToAdminCommand, PermissionDto>
        {
            private readonly UserRepository _repository;

            public PromoteToAdminCommandHandler(UserRepository repository)
            {
                _repository = repository;
            }

            public async Task<PermissionDto> Handle(PromoteToAdminCommand request, CancellationToken cancellationToken)
            {
                var identity = await _repository.ReadUserById(request.UserId);

                if (identity is null)
                    throw new NotFoundException(nameof(_repository.FindAsync), HttpMethod.Put, "user not found");


                switch (identity.Identity.Role)
                {
                    case UserType.Administrator:
                        throw new BadRequestException("account/promote", HttpMethod.Put,
                            "cannot-promote-admin-to-admin");
                    case UserType.Owner:
                        throw new BadRequestException("account/promote", HttpMethod.Put,
                            "cannot-promote-owner-to-admin");
                }

                identity.Identity.Role = UserType.Administrator;

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