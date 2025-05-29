using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.WebApp.Models.ViewModels
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public Role Role { get; set; }
        public AccessLevel AccessLevel { get; set; }
    }
}
