namespace Coworking.Application.Authentication.UserManager
{
    public class UserModel
    {
        public bool IsAuthorized => UserId != 0;
        public int UserId { get; set; } = 0;
        public int LocationId { get; set; }
        public int CompanyId { get; set; }
        public string Role { get; set; }
        public string Tenant { get; set; }
    }
}