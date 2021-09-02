using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Services;
using Coworking.Application.ViewModels.Product.Output;
using Coworking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers.CommercialController
{
    [Route("product/desk")]
    [ApiController]
    public class DeskController
    {
        private readonly ServerHubContext _context;
        private readonly IMediator _mediator;

        public DeskController(IMediator mediator, ServerHubContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [Cacheable]
        [Authorize(Policy = IdentityPolicies.WORKER_POLICY)]
        [HttpGet("get")]
        public async Task<IEnumerable<OutputDeskDto>> ReadDesksAsync()
        {
            return await _mediator.Send(new ReadDesksQuery());
        }


        [ManipulateCache("product/desk/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPost("add")]
        public async Task AddDeskAsync(AddDeskCommand command)
        {
            await _mediator.Send(command);
            await _context.InvokeActivityEvent("Desk added");
        }

        [ManipulateCache("product/desk/get")]
        [ManipulateCache("product/room/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("delete/{productId}")]
        public async Task DeleteProduct(int productId)
        {
            await _mediator.Send(new DeleteProductCommand(productId));
            await _context.InvokeActivityEvent("Product deleted");
        }

        [Authorize]
        [HttpGet("get/{productId}")]
        public async Task<OutputDeskDetailsDto> ProductDeskDetails(int productId)
        {
            return await _mediator.Send(new ReadDeskDetailsQuery(productId));
        }
    }
}