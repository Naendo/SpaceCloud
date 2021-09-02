using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class CompanyRepository : BaseRepository<Company>
    {
        public CompanyRepository(WorkingContext context) : base(context)
        {
        }


        public async Task<bool> CompanyExistingWithSubdomain(string subdomain)
        {
            return await Context.Companies.AnyAsync(x => x.SubDomain == subdomain);
        }
    }
}