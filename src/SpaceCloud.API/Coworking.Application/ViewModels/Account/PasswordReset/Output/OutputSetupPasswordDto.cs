namespace Coworking.Application
{
    public class OutputSetupPasswordDto
    {
        public string? RedirectUri { get; set; }
        public bool IsSuccessfull { get; set; }
        public string ResetToken { get; set; }
    }
}