namespace Coworking.Application.Services
{
    public class SendInvoiceMailViewModel : TemplateViewModel
    {
        public string? ProductImageUri { get; set; }
        public string ProductTitle { get; set; }
        public int ProductAmount { get; set; } = 1;
        public decimal ProductPrice { get; set; }
        public string Name { get; set; }
    }
}