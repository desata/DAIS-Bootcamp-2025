using WikiSystem.Models.Enums;

namespace WikiSystem.Services.DTOs.Authentication
{
    public class LoginResponse
    {

        public bool Success { get; set; }
        public string? Message { get; set; }
        public int? EmployeeId { get; set; }
        public string? FullName { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public Role Role { get; set; }

    }
}
