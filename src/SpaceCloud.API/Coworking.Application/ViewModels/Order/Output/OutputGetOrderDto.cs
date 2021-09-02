using System;
using Newtonsoft.Json;

namespace Coworking.Application.ViewModels
{
    public class OutputGetOrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public string Name => FirstName + " " + LastName;

        public DateTime Created { get; set; }

        [JsonIgnore] public string FirstName { private get; set; }

        [JsonIgnore] public string LastName { private get; set; }

        public string Status { get; set; }

        public decimal Price { get; set; }
    }
}