using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public class TicketRepository : BaseRepository<Ticket>
    {
        public TicketRepository(WorkingContext context) : base(context)
        {
        }


        public async Task<Ticket> GetTicketWithUser(Ticket ticket)
        {
            var result = await Context.Tickets
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.TicketId == ticket.TicketId);
            return result;
        }

        public async Task<List<Ticket>> GetAllTicketsIncludeUserAsync(int locationId)
        {
            return await Context.Tickets.Include(x => x.User)
                .Where(x => x.User.LocationId == locationId).ToListAsync();
        }
    }
}