using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.Interfaces;
using Coworking.Application.Services;
using Coworking.Domain.Entities;
using Coworking.Domain.Enumerations;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Coworking.Infrastructure
{
    public class RegisterCommand : IRequest<JwtPayloadModel>
    {
        public RegisterCommand(RegisterDto register)
        {
            Register = register;
        }

        public RegisterDto Register { get; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, JwtPayloadModel>
        {
            private readonly IHttpContextAccessor _contextAccessor;
            private readonly IHashService _hashService;
            private readonly ILogger<RegisterCommandHandler> _logger;
            private readonly MailService _mailService;
            private readonly AuthorizationRepository _repository;
            private readonly IBackgroundTaskQueue _taskQueue;
            private readonly UserAccessor _userAccessor;


            public RegisterCommandHandler(AuthorizationRepository repository, IHashService hashService,
                ILogger<RegisterCommandHandler> logger,
                IBackgroundTaskQueue taskQueue,
                MailService mailService,
                IHttpContextAccessor contextAccessor,
                UserAccessor userAccessor)
            {
                _repository = repository;
                _hashService = hashService;
                _logger = logger;
                _mailService = mailService;
                _contextAccessor = contextAccessor;
                _userAccessor = userAccessor;
                _taskQueue = taskQueue;
            }

            public async Task<JwtPayloadModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                //(1): Access Tenant
                var user = _userAccessor.BuildPayload();

                //(2): Validate
                if (await _repository.IsEmailExistingAsync(request.Register.Mail, user.Tenant))
                    throw new DuplicatedEntryException("account/register", HttpMethod.Post, "account-already-existing");

                //(3): Map
                var entity = new Identity
                {
                    Mail = request.Register.Mail,
                    User = new User
                    {
                        FirstName = request.Register.FirstName,
                        LastName = request.Register.LastName,
                        LocationId = await _repository.GetLocationIdWithTenant(user.Tenant)
                    }
                };

                //(4): Encrypt Password
                if (!_hashService.TryEncryptPasswordWithoutGivenSalt(request.Register.Password,
                    out var hashedCredentials))
                {
                    _logger.LogError(
                        $"[{nameof(_hashService.TryEncryptPasswordWithoutGivenSalt)}] has thrown a critical error! Password has not been generated!");
                    throw new InternalServerErrorException(nameof(_hashService.TryEncryptPasswordWithoutGivenSalt),
                        HttpMethod.Post, "password-generation-failed");
                }

                //(5): Mappings
                entity.Salt = hashedCredentials.SaltWithStringFormat;
                entity.Hash = hashedCredentials.Password;
                entity.Role = UserType.Worker;

                //(6): Database Actions
                entity = await _repository.AddAsync(entity);

                //(7): Send Mail
                _taskQueue.Enqueue(async x =>
                {
                    var test = await _mailService.BuildMailAsync(new AccountCreatedMailViewModel
                    {
                        Mail = request.Register.Mail,
                        ConfirmEmailUri = "https://www.google.at",
                        ImageUri = "https://workmate.blob.core.windows.net/images/Logo_email.png",
                        Name = entity.User.FirstName + " " + entity.User.LastName,
                        Subject = "Your SpaceCloud Account!"
                    }, MailType.AccountCreated);

                    await test.SendMail(x);
                });

                return new JwtPayloadModel
                {
                    LocationId = entity.User.LocationId,
                    UserId = entity.User.UserId,
                    Role = UserType.Worker.ToString()
                };
            }
        }
    }
}