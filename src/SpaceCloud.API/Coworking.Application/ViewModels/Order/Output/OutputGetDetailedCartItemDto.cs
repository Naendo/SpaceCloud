using System;

namespace Coworking.Application.ViewModels
{
    public class OutputGetDetailedCartItemDto
    {
        public string ProductName { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string ImageUri { get; set; }
        public string Usage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}