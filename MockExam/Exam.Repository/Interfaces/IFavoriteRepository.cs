using Exam.Models;

namespace Exam.Repository.Interfaces
{
    public interface IFavoriteRepository
    {
        Task<Favorite?> RetrieveByIdAsync(int favoriteId);
        Task<List<Favorite>> RetrieveByUserIdAsync(int userId);
        Task<int> CreateAsync(Favorite favorite);
        Task<bool> DeleteAsync(int favoriteId);

        // TODO: Check if this method is needed
        Task<List<Favorite>> RetrieveCollectionAsync();

        // Note: The following methods are not part of the original code but are included for completeness.
        Task<bool> UpdateAsync(Favorite favorite);
    }
}
