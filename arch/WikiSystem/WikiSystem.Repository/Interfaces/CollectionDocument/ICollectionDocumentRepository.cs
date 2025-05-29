using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.CollectionDocument
{
    public interface ICollectionDocumentRepository : IBaseRepository<Models.CollectionDocument, CollectionDocumentFilter, CollectionDocumentUpdate>
    {
        Task<bool> LinkAsync(int primaryId, int secondaryId);
        Task<int> LinkMultipleAsync(int primaryId, IEnumerable<int> secondaryIds);
    }
}
