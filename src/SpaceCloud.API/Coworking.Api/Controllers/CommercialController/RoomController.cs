using System;
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
    [Route("product/room")]
    [ApiController]
    public class RoomController
    {
        private readonly ServerHubContext _context;
        private readonly IMediator _mediator;

        public RoomController(IMediator mediator, ServerHubContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [Authorize(Policy = IdentityPolicies.WORKER_POLICY)]
        [HttpGet("get")]
        public async Task<IEnumerable<OutputRoomDto>> ReadRoomsAsync()
        {
            return await _mediator.Send(new ReadRoomsQuery());
        }

        [ManipulateCache("product/room/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPost("add")]
        public async Task AddRoomAsync(AddRoomCommand command)
        {
            await _mediator.Send(command);
            await _context.InvokeActivityEvent("Room added");
        }

        [ManipulateCache("product/desk/get")]
        [ManipulateCache("product/room/get")]
        [Authorize(Policy = IdentityPolicies.ADMIN_POLICY)]
        [HttpPut("delete/{productId}")]
        public Task UpdateRoom(int productId)
        {
            throw new NotImplementedException();
            //await _context.InvokeActivityEvent("Room updated");
        }


        [Authorize]
        [HttpGet("get/{productId}")]
        public async Task<OutputRoomDetailsDto> ProductDetails(int productId)
        {
            return await _mediator.Send(new ReadRoomDetailsQuery(productId));
        }
    }
}