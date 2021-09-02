using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public class RecentActionRepository : BaseRepository<RecentEvent>
    {
        public RecentActionRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<IEnumerable<RecentEvent>> GetRecentActionWithLocationId(int locationId)
        {
            return await Context.RecentEvents.Include(x => x.User)
                .Where(x => x.User.LocationId == locationId)
                .OrderByDescending(x => x.ActionId)
                .Take(25)
                .ToListAsync();
        }
    }
}