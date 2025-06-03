using DAIS.WikiSystem.Models;

namespace DAIS.WikiSystem.Repository.Interfaces.Mappers
{
    public interface ICollectionDocumentRepository
    {
        Task<bool> AddIfNotExistsAsync(int primaryId, int secondaryId);
        Task CreateMappingIfNotExistsAsync(CollectionDocument collectionDocument);
        Task<bool> RemoveAsync(int primaryId, int secondaryId);
    }
}
