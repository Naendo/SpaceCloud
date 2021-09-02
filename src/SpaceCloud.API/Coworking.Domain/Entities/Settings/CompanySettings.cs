namespace Coworking.Domain.Entities
{
    public class CompanySettings : BaseEntity
    {
        public int SettingsId { get; set; }
        public string Uid { get; set; }
        public int StartingInvoiceNr { get; set; }
        
        public int InvoiceNr { get; set; }
        public string LogoUri { get; set; }
        public string? PhoneNumber { get; set; }
        public string? WebsiteUri { get; set; }
        public string Iban { get; set; }


        public string ContactMail { get; set; }
        public string BankName { get; set; }
        public string BIC { get; set; }

        public int CurrencyAmountPerSubscription { get; set; }
        public Company Company { get; set; }
    }
}