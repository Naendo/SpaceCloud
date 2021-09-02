using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class AuthorizationRepository : BaseRepository<Identity>
    {
        public AuthorizationRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<Identity> ReadUserIdentityByMailAndCompanyIdAsync(InputAuthorizeDto dto, string subdomain)
        {
            return await Context.Identities
                .Include(x => x.User)
                .ThenInclude(x => x.Location)
                .ThenInclude(x => x.Company)
                .FirstOrDefaultAsync(x =>
                    x.Mail == dto.Mail && x.User.Location.Company.SubDomain == subdomain);
        }


        public async Task<bool> IsEmailExistingAsync(string mail, string tenant)
        {
            return await Context.Identities
                .Include(x => x.User)
                .ThenInclude(x => x.Location)
                .ThenInclude(x => x.Company)
                .AnyAsync(x => x.Mail == mail && x.User.Location.Company.SubDomain == tenant);
        }


        public async Task<List<Identity>> GetLatestUsers()
        {
            return await Context.Identities.Include(x => x.User).ToListAsync();
        }


        public async Task<int> GetCompanyIdByLocationId(int locationId)
        {
            return (await Context.CompanyLocations
                    .FirstOrDefaultAsync(x => x.LocationId == locationId))
                .CompanyId;
        }


        public async Task<int> GetLocationIdWithTenant(string tenant)
        {
            var company = await Context.Companies.Include(x => x.Locations)
                .FirstOrDefaultAsync(x => x.SubDomain == tenant);

            if (company is null)
                return -1;

            return company.Locations.FirstOrDefault().LocationId;
        }
    }
}