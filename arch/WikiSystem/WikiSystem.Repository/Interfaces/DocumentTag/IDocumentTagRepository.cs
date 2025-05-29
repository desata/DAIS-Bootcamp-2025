using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.DocumentTag
{
    public interface IDocumentTagRepository : IBaseRepository<Models.DocumentTag, DocumentTagFilter, DocumentTagUpdate>
    {
        Task<bool> LinkAsync(int primaryId, int secondaryId);
        Task<int> LinkMultipleAsync(int primaryId, IEnumerable<int> secondaryIds);
    }
}
