using ExamPrep.Models;

namespace ExamPrep.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetByUsernameAsync(string username);
    }
}
