namespace Coworking.Application
{
    public abstract class OutputProductDto
    {
        public int ProductId { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public string ImageUri { get; set; }
    }
}