using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class Employee
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(100)]
        public required string FullName { get; set; }
        [Required]
        [StringLength(50)]
        public required string Username { get; set; }
        [Required]
        [StringLength(256)]
        public required string PasswordHash { get; set; }
        [Required]
        public int Role { get; set; }
        [Required]
        public int AccessLevel { get; set; }
    }
}
