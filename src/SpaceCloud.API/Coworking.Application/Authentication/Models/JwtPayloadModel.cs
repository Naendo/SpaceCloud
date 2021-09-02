namespace Coworking.Application.Authentication
{
    public class JwtPayloadModel
    {
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public int CompanyId { get; set; }
        public string Role { get; set; }
    }
}