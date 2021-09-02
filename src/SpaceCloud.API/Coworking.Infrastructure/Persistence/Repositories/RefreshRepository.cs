using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class RefreshRepository : BaseRepository<Identity>
    {
        public RefreshRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<Identity> GetUserWithRefreshToken(string refreshToken)
        {
            return await Context.Identities.Include(x => x.User)
                .ThenInclude(x => x.Location)
                .ThenInclude(x => x.Company)
                .FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        }
    }
}