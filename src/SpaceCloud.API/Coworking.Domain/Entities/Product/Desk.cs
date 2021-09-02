using Coworking.Domain.Enumerations;

namespace Coworking.Domain.Entities
{
    public class Desk : BaseEntity
    {
        public int DeskId { get; set; }
        public DeskIntervalType IntervalType { get; set; }
        public int ProductAmount { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}