using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.ViewModels;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers.DoorViewController
{
    [ApiController]
    [Route("scheduler")]
    public class SchedulerController
    {
        private readonly IMediator _mediator;

        public SchedulerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //(1) scheduler/create
        //(2) scheduler/get
        //(3) scheduler/purchase
        //(4) scheduler/purchase/get
        //(5) scheduler/login


        [AllowAnonymous]
        [HttpPost("login/{token}")]
        public async Task<OutputAuthorizeDto> LoginScheduler(string token)
        {
            return await _mediator.Send(new AuthorizeSchedulerCommand(token));
        }


        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPost("create/{roomId}")]
        public async Task<OutputSchedulerLoginDto> CreateScheduler(int roomId)
        {
            return await _mediator.Send(new CreateSchedulerCommand(roomId));
        }


        [Authorize(Policy = IdentityPolicies.READONLY)]
        [HttpGet("get/{token}")]
        public async Task<OutputReadSchedulerDto> ReadScheduler(string token)
        {
            return await _mediator.Send(new ReadSchedulerQuery(token));
        }
    }
}