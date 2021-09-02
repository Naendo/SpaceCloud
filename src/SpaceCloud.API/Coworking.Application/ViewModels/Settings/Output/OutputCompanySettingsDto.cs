using System.Collections.Generic;

namespace Coworking.Application.ViewModels
{
    public class OutputCompanySettingsDto
    {
        public int CompanyId { get; set; }

        public string Uid { get; set; }

        public int StartingInvoiceNr { get; set; }

        public string LogoUri { get; set; }

        public string? PhoneNumber { get; set; }

        public string? WebsiteUri { get; set; }

        public string Iban { get; set; }


        public List<OutputCompanySettingsLocationAddressDto> Locations { get; set; }
    }

    public class OutputCompanySettingsLocationAddressDto
    {
        public int LocationId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public string? Door { get; set; }

        public string? Floor { get; set; }
    }
}