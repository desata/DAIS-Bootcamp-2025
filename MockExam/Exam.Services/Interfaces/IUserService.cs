using Exam.Models;
using Exam.Services.DTOs.User;

namespace Exam.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserInfo> GetByIdAsync(int userId);
        Task<List<UserInfo>> GetAllUsersAsync();
        Task<UserInfo?> GetByUsernameAsync(string username);
    }
}
