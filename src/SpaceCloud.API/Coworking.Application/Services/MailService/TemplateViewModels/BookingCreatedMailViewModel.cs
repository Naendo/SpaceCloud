using Coworking.Application.Services;

namespace Coworking.Application
{
    public class BookingCreatedMailViewModel : TemplateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => FirstName + " " + LastName;
        public string OrderNr { get; set; }
        public string OrderUri { get; set; }
    }
}