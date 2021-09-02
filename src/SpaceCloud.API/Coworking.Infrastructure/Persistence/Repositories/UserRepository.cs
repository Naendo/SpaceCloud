using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(WorkingContext context) : base(context)
        {
        }


        public async Task<List<User>> ReadUsersWithLocationId(int locationId)
        {
            return await Context.Users.Include(x => x.Identity)
                .Where(x => x.LocationId == locationId).ToListAsync();
        }


        public async Task<User> ReadUserById(int userId)
        {
            return await Context.Users.Include(x => x.Identity)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}