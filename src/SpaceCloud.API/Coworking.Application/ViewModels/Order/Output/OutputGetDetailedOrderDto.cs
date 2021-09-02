using System.Collections.Generic;
using Newtonsoft.Json;

namespace Coworking.Application.ViewModels
{
    public class OutputGetDetailedOrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public string Name => FirstName + " " + LastName;

        [JsonIgnore] public string FirstName { private get; set; }
        [JsonIgnore] public string LastName { private get; set; }

        public string Status { get; set; }
        public decimal Price { get; set; }
        public string PdfUri { get; set; }

        public IEnumerable<OutputGetDetailedCartItemDto> Items { get; set; }
    }
}