using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue;
using Coworking.Application;
using Coworking.Application.Services;
using Coworking.Domain.Enumerations;
using Coworking.Domain.Exceptions;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coworking.Infrastructure
{
    public class PasswordResetSetupCommand : IRequest<string>
    {
        public PasswordResetSetupCommand(InputSetupPasswordDto dto)
        {
            Dto = dto;
        }

        public InputSetupPasswordDto Dto { get; }

        public class PasswordResetSetupHandler : IRequestHandler<PasswordResetSetupCommand, string>
        {
            private readonly ILogger<PasswordResetSetupHandler> _logger;
            private readonly MailService _mailService;
            private readonly ITokenGenerator _reconsturctionService;
            private readonly AccountRepository _repository;
            private readonly IBackgroundTaskQueue _taskQueue;

            public PasswordResetSetupHandler(AccountRepository repository,
                IBackgroundTaskQueue taskQueue,
                ITokenGenerator tokenGenerator,
                ILogger<PasswordResetSetupHandler> logger,
                MailService mailService)
            {
                _repository = repository;
                _taskQueue = taskQueue;
                _reconsturctionService = tokenGenerator;
                _logger = logger;
                _mailService = mailService;
            }

            public async Task<string?> Handle(PasswordResetSetupCommand request, CancellationToken cancellationToken)
            {
                //Workflow:
                //01: Check if Account Exists

                var identity = await _repository.ReadIdentityWithMailAndCompanyId(request.Dto);

                if (identity.PasswordResetTokenExpires > DateTime.Now)
                    throw new BadRequestException(
                        nameof(_repository.ReadIdentityWithMailAndCompanyId) + " | " +
                        nameof(PasswordResetSetupHandler),
                        HttpMethod.Post, "user-has-reset-token");


                if (!MailService.IsValidMail(request.Dto.Mail))
                    throw new BadRequestException(nameof(MailService.IsValidMail), HttpMethod.Post,
                        "mail-has-wrong-format");

                //02: Insert Delete Token
                var token = _reconsturctionService.GenerateAndSetPasswordResetToken();

                identity.PasswordResetToken = token.Token;
                identity.PasswordResetTokenExpires = token.Expires;

                //03: Update Db
                await _repository.UpdateAsync(identity);

                //04: Send Mail
                _taskQueue.Enqueue(async x =>
                {
                    var test = await _mailService.BuildMailAsync(new PasswordResetMailViewModel
                    {
                        FirstName = identity.User.FirstName,
                        LastName = identity.User.LastName,
                        Mail = request.Dto.Mail,
                        ResetUri = "https://www.google.at",
                        ImageUri = "https://workmate.blob.core.windows.net/images/headerimage.png",
                        Subject = "How to reset your SpaceCloud Password"
                    }, MailType.PasswordReset);
                    await test.SendMail(x);
                    _logger.LogInformation($"[{DateTime.Now}]: mail of type [{MailType.PasswordReset}] sent");
                });


                return token.Token;
            }
        }
    }
}