namespace Coworking.Application
{
    public class InputAuthorizeDto
    {
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool StayLogged { get; set; }
    }
}