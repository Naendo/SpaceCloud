using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Services;
using Coworking.Infrastructure;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    public class AccountController
    {
        private readonly ServerHubContext _hub;
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, ServerHubContext hub)
        {
            _mediator = mediator;
            _hub = hub;
        }


        /// <returns cref="OutputSetupPasswordDto"></returns>
        [HttpPost("account/password/reset/setup")]
        public async Task<string> PasswordResetSetup(InputSetupPasswordDto dto, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new PasswordResetSetupCommand(dto), cancellationToken);
        }

        [HttpPost("account/password/reset/resend")]
        public async Task<OutputSetupPasswordDto> ResendPasswordResetMail(InputSetupPasswordDto dto)
        {
            return await _mediator.Send(new ResendPasswordResetMailCommand(dto));
        }

        [HttpPost("account/password/reset")]
        public async Task PasswordResetWithMailConfirmation([FromBody] InputPasswordResetWithTokenDto model)
        {
            await _mediator.Send(new PasswordResetWithTokenCommand(model));
        }


        [ManipulateCache("user/get")]
        [Authorize]
        [HttpDelete("account/delete")]
        public async Task DeleteOwnAccount()
        {
            await _hub.InvokeActivityEvent("Account deleted");
            await _mediator.Send(new DeleteUserCommand());
        }


        [ManipulateCache("user/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpDelete("account/delete/{userId}")]
        public async Task DeleteAccountById(int userId)
        {
            await _hub.InvokeActivityEvent("Account deleted");
            await _mediator.Send(new DeleteUserCommand(userId));
        }


        [ManipulateCache("user/get")]
        [HttpPost("account/register")]
        public async Task Register(RegisterDto register)
        {
            var payload = await _mediator.Send(new RegisterCommand(register));
            await _hub.InvokeActivityEvent("Account Registered", payload);
        }


        [ManipulateCache("user/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("account/promote/{id}")]
        public async Task<PermissionDto> PromoteToAdmin(int id)
        {
            await _hub.InvokeActivityEvent("Promote to Admin");
            return await _mediator.Send(new PromoteToAdminCommand(id));
        }


        [ManipulateCache("user/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("account/demote/{id}")]
        public async Task<PermissionDto> DemoteToUser(int id)
        {
            var result = await _mediator.Send(new DemoteToWorkerCommand(id));
            await _hub.InvokeActivityEvent("Demote to Worker");
            return result;
        }
    }
}