using Coworking.Domain.Enumerations;

namespace Coworking.Application
{
    public class OutputUserDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string ImageUri { get; set; }

        public string Role { get; set; }
    }
}