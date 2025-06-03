using ExamPrep.Services.DTOs.Authentication;

namespace ExamPrep.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}