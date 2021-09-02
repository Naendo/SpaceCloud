using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Application.Authentication;
using Coworking.Application.ViewModels;
using Coworking.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    [Route("worker")]
    public class WorkerController
    {
        private readonly IMediator _mediator;

        public WorkerController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Authorize(Policy = IdentityPolicies.WORKER_POLICY)]
        [HttpGet("order/get")]
        public async Task<IEnumerable<OutputGetOrderDto>> GetOrdersAsync()
        {
            return await _mediator.Send(new GetOrderOfWorkerQuery());
        }
    }
}