namespace Coworking.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public int InvoiceId { get; set; }
        public string PdfUri { get; set; }


        public int InvoiceNr { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}