using DAIS.WikiSystem.Services.DTOs.Authentication;

namespace DAIS.WikiSystem.Services.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
