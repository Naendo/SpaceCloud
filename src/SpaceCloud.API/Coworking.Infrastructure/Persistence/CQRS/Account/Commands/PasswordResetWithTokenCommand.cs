using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Interfaces;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coworking.Infrastructure
{
    public class PasswordResetWithTokenCommand : IRequest
    {
        public PasswordResetWithTokenCommand(InputPasswordResetWithTokenDto dto)
        {
            Dto = dto;
        }

        public InputPasswordResetWithTokenDto Dto { get; }

        public class PasswordResetWithTokenHandler : IRequestHandler<PasswordResetWithTokenCommand>
        {
            private readonly IHashService _hashService;
            private readonly ILogger<PasswordResetWithTokenHandler> _logger;
            private readonly AccountRepository _repository;

            public PasswordResetWithTokenHandler(AccountRepository repository,
                IHashService hashService,
                ILogger<PasswordResetWithTokenHandler> logger)

            {
                _repository = repository;
                _hashService = hashService;
                _logger = logger;
            }


            public async Task<Unit> Handle(PasswordResetWithTokenCommand request, CancellationToken cancellationToken)
            {
                //Workflow:
                //01: Check if Account Exists
                //01: Check if mail exists
                var entity = await _repository.GetUserWithMailAndResetToken(request.Dto);

                if (entity is null)
                    throw new NotFoundException(nameof(_repository.GetUserWithMailAndResetToken), HttpMethod.Post,
                        "user-not-found-by-mail");

                //02: Generate Password
                if (!_hashService.TryEncryptPasswordWithoutGivenSalt(request.Dto.Password, out var hashedCredentials))
                {
                    _logger.LogError(nameof(_hashService.TryEncryptPasswordWithGivenSalt) + " has thrown an error");
                    throw new InternalServerErrorException(nameof(_hashService.TryEncryptPasswordWithoutGivenSalt),
                        HttpMethod.Post, "password-encryption-failed");
                }

                //03: Insert UserIdentity Object
                entity.Hash = hashedCredentials.Password;
                entity.Salt = hashedCredentials.SaltWithStringFormat;
                entity.PasswordResetToken = null;
                entity.PasswordResetTokenExpires = null;

                await _repository.UpdateAsync(entity);
                return Unit.Value;
            }
        }
    }
}