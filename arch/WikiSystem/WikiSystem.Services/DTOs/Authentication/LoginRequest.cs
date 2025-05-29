namespace WikiSystem.Services.DTOs.Authentication
{
    public class LoginRequest
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }

    }
}
