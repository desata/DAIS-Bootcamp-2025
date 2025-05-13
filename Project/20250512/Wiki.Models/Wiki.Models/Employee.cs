using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Wiki.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Userame is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Username must be between 2 and 50 characters")]
        public required string Username { get; set; }

        [Required]
        [StringLength(256)]
        public required string Password { get; set; }

        public string? Picture { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public Role Role { get; set; }

        [Required(ErrorMessage = "Access level is required")]
        public AccessLevel AccessLevel { get; set; }
    }
}