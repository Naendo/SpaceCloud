using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.Authentication.UserManager;
using Coworking.Application.ViewModels;
using MediatR;

namespace Coworking.Infrastructure
{
    public class GetSettingsQuery : IRequest<OutputCompanySettingsDto>
    {
        public class GetSettingsQueryHandler : IRequestHandler<GetSettingsQuery, OutputCompanySettingsDto>
        {
            private readonly UserAccessor _accessor;
            private readonly SettingsRepository _repository;

            public GetSettingsQueryHandler(SettingsRepository repository, UserAccessor accessor)
            {
                _repository = repository;
                _accessor = accessor;
            }

            public async Task<OutputCompanySettingsDto> Handle(GetSettingsQuery request,
                CancellationToken cancellationToken)
            {
                var payload = _accessor.BuildPayload();
                var result = await _repository.GetSettingsByCompanyId(payload.CompanyId);

                return new OutputCompanySettingsDto
                {
                    CompanyId = result.Company.CompanyId,
                    Iban = result.Iban,
                    LogoUri = result.LogoUri,
                    StartingInvoiceNr = result.StartingInvoiceNr,
                    Uid = result.Uid,
                    PhoneNumber = result.PhoneNumber,
                    WebsiteUri = result.WebsiteUri,
                    Locations = result.Company.Locations.Select(x => new OutputCompanySettingsLocationAddressDto
                    {
                        Address = x.Address,
                        City = x.City,
                        Door = x.Door,
                        Floor = x.Floor,
                        LocationId = x.LocationId,
                        Zip = x.Zip
                    }).ToList()
                };
            }
        }
    }
}