using Exam.Services.DTOs.Authentication;

namespace Exam.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}
