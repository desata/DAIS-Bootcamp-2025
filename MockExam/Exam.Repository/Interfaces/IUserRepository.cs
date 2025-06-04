using Exam.Models;

namespace Exam.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> RetrieveByIdAsync(int userId);
        Task<User?> RetrieveByUsernameAsync(string username);
        Task<List<User>> RetrieveCollectionAsync();

        // Note: The following methods are not part of the original code but are included for completeness.
        Task<int> CreateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<bool> UpdateAsync(User user);
    }
}