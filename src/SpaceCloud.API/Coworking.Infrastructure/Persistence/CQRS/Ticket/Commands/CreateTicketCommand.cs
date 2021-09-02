using System.Threading;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Application.Authentication.UserManager;
using Coworking.Domain.Entities;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class CreateTicketCommand : IRequest<Ticket>
    {
        public CreateTicketCommand(InputTicketDto ticket)
        {
            Ticket = ticket;
        }

        public InputTicketDto Ticket { get; }


        public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, Ticket>
        {
            private readonly TicketRepository _repository;
            private readonly UserAccessor _userAccessor;

            public CreateTicketCommandHandler(TicketRepository repository, UserAccessor userAccessor)
            {
                _repository = repository;
                _userAccessor = userAccessor;
            }

            public async Task<Ticket> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
            {
                var payload = _userAccessor.BuildPayload();

                var result = new Ticket
                {
                    Subject = request.Ticket.Subject,
                    Content = request.Ticket.Content,
                    UserId = payload.UserId
                };

                var repositoryResult = await _repository.AddAsync(result);

                return await _repository.GetTicketWithUser(repositoryResult);
            }
        }
    }
}