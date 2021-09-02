using System.Threading.Tasks;
using Coworking.Application.Authentication;
using Coworking.Application.Services;
using Coworking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers.BookingController
{
    [ApiController]
    [Route("product")]
    public class ProductController
    {
        private readonly ServerHubContext _context;
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator, ServerHubContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [ManipulateCache("product/desk/get")]
        [ManipulateCache("product/room/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("disable/{productId}")]
        public async Task DisableProduct(int productId)
        {
            await _mediator.Send(new DisableProductCommand(productId));
            await _context.InvokeActivityEvent("Product disabled");
        }


        [ManipulateCache("product/desk/get")]
        [ManipulateCache("product/room/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("enable/{productId}")]
        public async Task EnableProduct(int productId)
        {
            await _mediator.Send(new EnableProductCommand(productId));
            await _context.InvokeActivityEvent("Product enabled");
        }


        [ManipulateCache("product/desk/get")]
        [ManipulateCache("product/room/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpDelete("delete/{productId}")]
        public async Task DeleteProduct(int productId)
        {
            await _mediator.Send(new DeleteProductCommand(productId));
        }
    }
}