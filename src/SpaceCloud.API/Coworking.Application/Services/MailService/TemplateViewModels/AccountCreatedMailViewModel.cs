using Coworking.Application.Services;

namespace Coworking.Application
{
    public class AccountCreatedMailViewModel : TemplateViewModel
    {
        public string ConfirmEmailUri { get; set; }
        public string Name { get; set; }
    }
}