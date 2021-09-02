using System.Collections.Generic;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public class DashboardRepository : BaseRepository<User>
    {
        public DashboardRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<List<User>> FetchLatestUsersAsync()
        {
            return await Context.Users.ToListAsync();
        }
    }
}