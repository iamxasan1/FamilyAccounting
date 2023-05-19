using FamilyAccounting.Data.Entities;

namespace FamilyAccouting.Models
{
    public class SignUpUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public EUserType UserType { get; set; }
        public IFormFile Photo { get; set; }
        public string Password { get; set; }
    }
}
