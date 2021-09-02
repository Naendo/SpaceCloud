using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Coworking.Application.ViewModels;
using Coworking.Domain.Entities;
using MediatR;

namespace Coworking.Infrastructure
{
    public class UpdateCompanySettingsCommand : IRequest
    {
        public UpdateCompanySettingsCommand(OutputCompanySettingsDto settings)
        {
            Settings = settings;
        }

        private OutputCompanySettingsDto Settings { get; }

        public class UpdateCompanySettingsCommandHandler : IRequestHandler<UpdateCompanySettingsCommand>
        {
            private readonly SettingsRepository _repository;

            public UpdateCompanySettingsCommandHandler(SettingsRepository repository)
            {
                _repository = repository;
            }

            public async Task<Unit> Handle(UpdateCompanySettingsCommand request,
                CancellationToken cancellationToken)
            {
                var company = await _repository.GetCompanyAsync(request.Settings.CompanyId);

                company.Settings = new CompanySettings
                {
                    Iban = request.Settings.Iban,
                    LogoUri = request.Settings.LogoUri,
                    PhoneNumber = request.Settings.PhoneNumber,
                    SettingsId = company.SettingsId,
                    StartingInvoiceNr = request.Settings.StartingInvoiceNr,
                    Uid = request.Settings.Uid,
                    WebsiteUri = request.Settings.WebsiteUri
                };


                var locations = new List<CompanyLocation>();

                request.Settings.Locations.ForEach(x =>
                {
                    locations.Add(new CompanyLocation
                    {
                        LocationId = x.LocationId,
                        Address = x.Address,
                        CompanyId = company.CompanyId,
                        City = x.City,
                        Door = x.Door,
                        Zip = x.Zip
                    });
                });

                company.Locations = locations;
                await _repository.UpdateCompanyAsync(company);

                return Unit.Value;
            }
        }
    }
}