using System.ComponentModel.DataAnnotations;
using System.Data;
using Wiki.Models.Enums;

namespace Wiki.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
        public required string FullName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._-]+$", ErrorMessage = "Username can only contain letters, numbers, dots, underscores and dashes")]
        public required string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(256)]
        public required string PasswordHash { get; set; }

        [Required(ErrorMessage = "Role type is required")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Access level is required")]
        public AccessLevel AccessLevel { get; set; }
    }
}