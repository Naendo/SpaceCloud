namespace Coworking.Application
{
    public class InputPasswordResetWithTokenDto
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public string ResetToken { get; set; }
        public int CompanyId { get; set; }
    }
}