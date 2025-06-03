using DAIS.WikiSystem.Services.DTOs.User;

namespace DAIS.WikiSystem.Services.Interfaces.User
{
    public interface IUserService
    {
        Task<GetUserResponse> GetByIdAsync(int userId);
    }
}