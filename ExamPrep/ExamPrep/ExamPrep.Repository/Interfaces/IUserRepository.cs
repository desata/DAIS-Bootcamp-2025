using ExamPrep.Models;

namespace ExamPrep.Repository.Interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<List<User>> GetAllUsersAsync();

        // Note: The following methods are not part of the original code but are included for completeness.
        Task<int> CreateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(User user);
    }
}