using System.Text.Json.Serialization;
using Coworking.Application.Services;

namespace Coworking.Application
{
    public class DeclineOrderMailViewModel : TemplateViewModel
    {
        [JsonIgnore] public string FirstName { get; set; }

        [JsonIgnore] public string LastName { get; set; }

        public string Name => FirstName + " " + LastName;
        public string OrderNr { get; set; }
        public string ProductImageUri { get; set; }
        public string ProductTitle { get; set; }
        public string ProductAmount { get; set; }
        public string ProductPrice { get; set; }
        public string DeclineReason { get; set; }
    }
}