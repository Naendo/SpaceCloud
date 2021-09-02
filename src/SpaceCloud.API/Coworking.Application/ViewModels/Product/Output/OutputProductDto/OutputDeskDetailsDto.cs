using Coworking.Domain.Enumerations;

namespace Coworking.Application.ViewModels.Product.Output
{
    public class OutputDeskDetailsDto
    {
        public int ProductId { get; set; }
        public int DeskId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public decimal Price { get; set; }
        public DeskIntervalType IntervalType { get; set; }

        public int AvailableAmount { get; set; }
        public bool CurrentlyAvailable { get; set; }
    }
}