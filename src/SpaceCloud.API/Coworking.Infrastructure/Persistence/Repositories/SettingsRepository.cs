using System.Threading.Tasks;
using Coworking.Domain.Entities;
using Coworking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure
{
    public class SettingsRepository : BaseRepository<CompanySettings>
    {
        public SettingsRepository(WorkingContext context) : base(context)
        {
        }


        public async Task<CompanySettings> GetSettingsByCompanyId(int companyId)
        {
            var result = await Context.Companies
                .Include(x => x.Settings)
                .Include(x => x.Locations)
                .FirstOrDefaultAsync(x => x.CompanyId == companyId);
            return result.Settings;
        }

        public async Task<Company> GetCompanyAsync(int companyId)
        {
            return await Context.Companies.FirstOrDefaultAsync(x => x.CompanyId == companyId);
        }


        public async Task UpdateCompanyAsync(Company company)
        {
            Context.Companies.Update(company);
            await Context.SaveChangesAsync();
        }


        public async Task<CompanyLocation> GetCompanyLocationAsync(int locationId)
        {
            return await Context.CompanyLocations.Include(x => x.Company)
                .FirstOrDefaultAsync(x => x.LocationId == locationId);
        }

        public async Task<int> ReadAndUpdateInvoiceNrAsync(int companyId)
        {
            var value = await Context.Companies
                .Include(x => x.Settings)
                .FirstOrDefaultAsync(x => x.CompanyId == companyId);

            if (value.Settings.InvoiceNr is 0)
                value.Settings.InvoiceNr = value.Settings.StartingInvoiceNr;

            value.Settings.InvoiceNr++;
            Context.CompanySettings.Update(value.Settings);
            await Context.SaveChangesAsync();

            return value.Settings.InvoiceNr;
        }
    }
}