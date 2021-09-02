using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Services;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    [Route("ticket")]
    public class TicketController
    {
        private readonly ServerHubContext _dashboard;
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator, ServerHubContext dashboard)
        {
            _mediator = mediator;
            _dashboard = dashboard;
        }


        [ManipulateCache("ticket/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPost("add")]
        public async Task CreateTicket(InputTicketDto dto)
        {
            var result = await _mediator.Send(new CreateTicketCommand(dto));
            await _dashboard.InvokeTicketCreatedEventAsync(result);
        }


        [ManipulateCache("ticket/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpDelete("delete/{id}")]
        public async Task DeleteTicket(int id)
        {
            await _mediator.Send(new DeleteTicketCommand(id));
        }


        [Cacheable]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("get")]
        public async Task<IEnumerable<OutputTicketDto>> ReadTickets()
        {
            return await _mediator.Send(new ReadTicketsQuery());
        }
    }
}