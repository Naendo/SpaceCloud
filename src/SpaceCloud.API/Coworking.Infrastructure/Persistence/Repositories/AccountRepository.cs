using System.Net.Http;
using System.Threading.Tasks;
using Coworking.Application;
using Coworking.Domain.Entities;
using Coworking.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Coworking.Infrastructure.Persistence
{
    public class AccountRepository : BaseRepository<Identity>
    {
        public AccountRepository(WorkingContext context) : base(context)
        {
        }

        public async Task<Identity> GetUserWithMailAdress(InputSetupPasswordDto dto)
        {
            return await Context.Identities.Include(x => x.User)
                .ThenInclude(x => x.Location)
                .FirstOrDefaultAsync(x => x.Mail == dto.Mail && x.User.Location.CompanyId == dto.CompanyId);
        }


        public async Task<Identity> ReadIdentityWithMailAndCompanyId(InputSetupPasswordDto dto)
        {
            var identityByCompanyIdAndMail = await Context.Identities.Include(x => x.User)
                .ThenInclude(x => x.Location)
                .FirstOrDefaultAsync(x => x.Mail == dto.Mail && x.User.Location.CompanyId == dto.CompanyId);

            if (identityByCompanyIdAndMail is null)
                throw new NotFoundException("reset/password", HttpMethod.Put, "user-does-not-exist");

            return identityByCompanyIdAndMail;
        }


        public async Task<Identity> GetUserWithMailAndResetToken(InputPasswordResetWithTokenDto dto)
        {
            return await Context.Identities.Include(x => x.User)
                .ThenInclude(x => x.Location)
                .ThenInclude(x => x.Company)
                .FirstOrDefaultAsync(x =>
                    x.PasswordResetToken == dto.ResetToken && dto.Mail == x.Mail &&
                    x.User.LocationId == dto.CompanyId);
        }
    }
}