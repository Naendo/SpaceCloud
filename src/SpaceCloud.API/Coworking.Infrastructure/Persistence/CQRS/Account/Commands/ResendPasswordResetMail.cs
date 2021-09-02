using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue;
using Coworking.Application;
using Coworking.Application.Services;
using Coworking.Domain.Enumerations;
using Coworking.Domain.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Coworking.Infrastructure.Persistence
{
    public class ResendPasswordResetMailCommand : IRequest<OutputSetupPasswordDto>
    {
        public ResendPasswordResetMailCommand(InputSetupPasswordDto dto)
        {
            Dto = dto;
        }

        public InputSetupPasswordDto Dto { get; }

        public class
            ResendPasswordResetMailCommandHandler : IRequestHandler<ResendPasswordResetMailCommand,
                OutputSetupPasswordDto>
        {
            private readonly ILogger<ResendPasswordResetMailCommandHandler> _logger;
            private readonly MailService _mailService;
            private readonly AccountRepository _repository;
            private readonly IBackgroundTaskQueue _taskQueue;
            private readonly ITokenGenerator _tokenGenerator;

            public ResendPasswordResetMailCommandHandler(AccountRepository repository,
                IBackgroundTaskQueue taskQueue,
                ITokenGenerator tokenGenerator,
                MailService mailService,
                ILogger<ResendPasswordResetMailCommandHandler> logger)
            {
                _repository = repository;
                _taskQueue = taskQueue;
                _tokenGenerator = tokenGenerator;
                _logger = logger;
                _mailService = mailService;
            }

            public async Task<OutputSetupPasswordDto> Handle(ResendPasswordResetMailCommand request,
                CancellationToken cancellationToken)
            {
                var token = await _repository.ReadIdentityWithMailAndCompanyId(request.Dto);

                if (token.PasswordResetToken is null || token.PasswordResetTokenExpires > DateTime.Now)
                    throw new NotFoundException("account/password/reset/resend", HttpMethod.Post, "no-reset-token");

                var entity = await _repository.GetUserWithMailAdress(request.Dto);

                var resetToken = _tokenGenerator.GenerateAndSetPasswordResetToken();
                entity.PasswordResetToken = resetToken.Token;
                entity.PasswordResetTokenExpires = resetToken.Expires;


                _taskQueue.Enqueue(async cancToken =>
                {
                    var test = await _mailService.BuildMailAsync(new PasswordResetMailViewModel
                    {
                        FirstName = entity.User.FirstName,
                        LastName = entity.User.LastName,
                        Mail = request.Dto.Mail,
                        ResetUri = "https://www.google.at",
                        ImageUri = "https://workmate.blob.core.windows.net/images/headerimage.png",
                        Subject = "How to reset your SpaceCloud Password"
                    }, MailType.PasswordReset);
                    await test.SendMail(cancToken);
                    _logger.LogInformation($"[{DateTime.Now}]: mail of type [{MailType.PasswordReset}] resent");
                });

                await _repository.UpdateAsync(entity);

                return new OutputSetupPasswordDto
                {
                    IsSuccessfull = true,
                    ResetToken = entity.PasswordResetToken,
                    RedirectUri = null
                };
            }
        }
    }
}