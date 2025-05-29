using WikiSystem.Services.DTOs.Authentication;

namespace WikiSystem.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
