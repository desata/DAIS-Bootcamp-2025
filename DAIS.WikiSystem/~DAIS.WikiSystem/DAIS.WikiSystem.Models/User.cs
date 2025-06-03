using DAIS.WikiSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name must be between 2 and 50 characters")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(256)]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Access level is required")]
        public AccessLevel AccessLevel { get; set; }
    }
}
