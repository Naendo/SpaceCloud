using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.ViewModels;
using Coworking.Infrastructure;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    [Route("dashboard/fetch")]
    public class DashboardController
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("actions")]
        public async Task<IEnumerable<RecentEventDto>> FetchAction()
        {
            return await _mediator.Send(new FetchActionsQuery());
        }

        [Authorize(IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("tickets")]
        public async Task<List<OutputTicketDto>> FetchTickets()
        {
            return await _mediator.Send(new ReadTicketsQuery());
        }

        [Authorize(IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("usage")]
        public async Task<UsageEventDto> FetchUsage()
        {
            return await _mediator.Send(new FetchCoworkingUsageQuery());
        }
    }
}