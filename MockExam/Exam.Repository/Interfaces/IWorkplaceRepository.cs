using Exam.Models;

namespace Exam.Repository.Interfaces
{
    public interface IWorkplaceRepository
    {
        Task<Workplace?> RetrieveByIdAsync(int workplaceId);
        Task<List<Workplace>> RetrieveCollectionAsync();
        Task<bool> UpdateAsync(Workplace workplace);

        // Note: The following methods are not part of the original code but are included for completeness.
        Task<int> CreateAsync(Workplace workplace);
        Task<bool> DeleteAsync(int workplaceId);

    }
}