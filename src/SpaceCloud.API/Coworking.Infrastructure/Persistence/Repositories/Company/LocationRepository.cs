using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class LocationRepository : BaseRepository<CompanyLocation>
    {
        public LocationRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<CompanyLocation> GetCompanyLocationByLocationIdAsync(int locationId)
        {
            return await Context.CompanyLocations.FirstOrDefaultAsync(x => x.LocationId == locationId);
        }
    }
}