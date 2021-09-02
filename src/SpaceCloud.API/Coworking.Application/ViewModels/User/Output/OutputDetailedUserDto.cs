using Coworking.Domain.Enumerations;

namespace Coworking.Application
{
    public class OutputUserDetailDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUri { get; set; }

        public UserType Role { get; set; }

        public bool IsPermittedUser { get; set; }
        public string Mail { get; set; }
    }
}