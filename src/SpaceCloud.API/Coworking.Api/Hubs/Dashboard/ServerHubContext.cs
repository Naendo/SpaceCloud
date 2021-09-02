using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication;
using Coworking.Application.Authentication.UserManager;
using Coworking.Domain.Entities;
using Coworking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Coworking.Api
{
    public class ServerHubContext
    {
        private readonly IHubContext<DashboardHub> _hubContext;
        private readonly IMediator _mediator;
        private readonly UserAccessor _userAccessor;


        public ServerHubContext(IHubContext<DashboardHub> hubContext, IMediator mediator, UserAccessor userAccessor)
        {
            _hubContext = hubContext;
            _mediator = mediator;
            _userAccessor = userAccessor;
        }


        public async Task InvokeTicketCreatedEventAsync(Ticket ticket)
        {
            var locationId = _userAccessor.BuildPayload()?.LocationId;

            var user = await _mediator.Send(new ReadUserQuery(ticket.UserId));

            await _hubContext.Clients.Group(locationId.ToString())
                .SendAsync("OnTicket", new OutputTicketDto
                {
                    Content = ticket.Content,
                    CreationDate = ticket.CreatedAt,
                    Subject = ticket.Subject,
                    TicketId = ticket.TicketId,
                    UserId = ticket.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                });
        }


        public async Task InvokeActivityEvent(string action)
        {
            var locationId = _userAccessor.BuildPayload()?.LocationId;

            var entity = await _mediator.Send(new AddRecentActionCommand(action));
            await _hubContext.Clients.Group(locationId.ToString())
                .SendAsync("OnActivity", entity);
        }

        /// <summary>
        ///     ONLY for endpoints without <see cref="AuthorizeAttribute" />
        /// </summary>
        public async Task InvokeActivityEvent(string action, JwtPayloadModel model)
        {
            var locationId = model.LocationId;

            var entity = await _mediator.Send(new AddRecentActionCommand(action, model));
            await _hubContext.Clients.Group(locationId.ToString())
                .SendAsync("OnActivity", entity);
        }


        public async Task InvokeUsageEvent()
        {
            var locationId = _userAccessor.BuildPayload()?.LocationId;

            var result = _mediator.Send(new FetchCoworkingUsageQuery());
            await _hubContext.Clients.Group(locationId.ToString())
                .SendAsync("OnUsage", result);
        }
    }
}