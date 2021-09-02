using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Coworking.Infrastructure.Persistence
{
    public class DeleteTicketCommand : IRequest
    {
        public DeleteTicketCommand(int ticketId)
        {
            TicketId = ticketId;
        }

        public int TicketId { get; }

        public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
        {
            private readonly TicketRepository _repository;

            public DeleteTicketCommandHandler(TicketRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
            {
                await _repository.DeleteAsync(request.TicketId);
                return Unit.Value;
            }
        }
    }
}