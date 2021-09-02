using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Services;
using Coworking.Application.ViewModels;
using Coworking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coworking.Api.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController
    {
        private readonly ServerHubContext _context;
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator, ServerHubContext context)
        {
            _mediator = mediator;
            _context = context;
        }


        [ManipulateCache("order/get")]
        [Authorize(Policy = IdentityPolicies.WORKER_POLICY)]
        [HttpPost("add")]
        public async Task AddOrderAsync(IEnumerable<InputAddOrderDto> items)
        {
            await _mediator.Send(new AddOrderCommand(items));
            await _context.InvokeActivityEvent("Order created");
        }


        [Cacheable]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("get")]
        public async Task<IEnumerable<OutputGetOrderDto>> GetAllOrdersAsync()
        {
            return await _mediator.Send(new ReadOrdersAsyncQuery());
        }


        [ManipulateCache("order/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("decline/{orderId}")]
        public async Task<OutputGetOrderDto> DeclineOrder(int orderId)
        {
            var result = await _mediator.Send(new DeclineOrderCommand(orderId));
            await _context.InvokeActivityEvent("Order declined");
            return result;
        }


        [ManipulateCache("order/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("accept/{orderId}")]
        public async Task<OutputGetOrderDto> AcceptOrder(int orderId)
        {
            var result = await _mediator.Send(new AcceptOrderCommand(orderId));
            await _context.InvokeActivityEvent("Order billed");
            await _context.InvokeUsageEvent();
            return result;
        }


        [ManipulateCache("order/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("paid/{orderId}")]
        public async Task<OutputGetOrderDto> PayOrder(int orderId)
        {
            var result = await _mediator.Send(new MarkOrderPayedCommand(orderId));
            await _context.InvokeActivityEvent("Order payed");
            return result;
        }

        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpGet("get/{orderId}")]
        public async Task<OutputGetDetailedOrderDto> GetOrderAsync(int orderId)
        {
            return await _mediator.Send(new GetOrderDetailAsyncQuery(orderId));
        }
    }
}