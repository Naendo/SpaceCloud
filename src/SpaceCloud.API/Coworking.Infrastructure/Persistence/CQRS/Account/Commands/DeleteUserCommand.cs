using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coworking.Infrastructure
{
    public class DeleteUserCommand : IRequest
    {
        public DeleteUserCommand(int userId)
        {
            UserId = userId;
        }

        public DeleteUserCommand()
        {
        }

        public int? UserId { get; }

        public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
        {
            private readonly ILogger<DeleteUserCommandHandler> _logger;

            private readonly UserRepository _repository;
            private readonly UserAccessor _userAccessor;

            public DeleteUserCommandHandler(UserRepository repository, ILogger<DeleteUserCommandHandler> logger,
                UserAccessor userAccessor)
            {
                _repository = repository;
                _logger = logger;
                _userAccessor = userAccessor;
            }

            public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
            {
                var userId = request.UserId ?? _userAccessor.BuildPayload().UserId;

                var result = await _repository.FindAsync(userId);
                if (result is null)
                    throw new NotFoundException(nameof(_repository.FindAsync), HttpMethod.Delete,
                        "user-with-id-was-not-found");

                await _repository.DeleteAsync(result);
                _logger.LogInformation($"[{DateTime.Now}]User: #{result.UserId} has been deleted");

                return Unit.Value;
            }
        }
    }
}