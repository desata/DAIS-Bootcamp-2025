using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.Services.DTOs.Authentication
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Role? Role { get; set; }
        public AccessLevel? AccessLevel { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
