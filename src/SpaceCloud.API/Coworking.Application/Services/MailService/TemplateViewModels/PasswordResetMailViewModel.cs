namespace Coworking.Application.Services
{
    public class PasswordResetMailViewModel : TemplateViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name => $"{FirstName} {LastName}";
        public string ResetUri { get; set; }
    }
}