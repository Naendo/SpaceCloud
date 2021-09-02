using System;
using System.Threading.Tasks;
using Coworking.Application.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Coworking.Api
{
    [Authorize]
    [AllowAnonymous]
    public class DashboardHub : Hub
    {
        private readonly IMediator _mediator;

        public DashboardHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Groups.RemoveFromGroupAsync(Context.ConnectionId,
                Context.User.FindFirst(nameof(JwtPayloadModel.LocationId)).Value);
            return base.OnDisconnectedAsync(exception);
        }

        public override Task OnConnectedAsync()
        {
            Groups.AddToGroupAsync(Context.ConnectionId,
                Context.User.FindFirst(nameof(JwtPayloadModel.LocationId)).Value);
            return base.OnConnectedAsync();
        }
    }
}