using DAIS.WikiSystem.Models;

namespace DAIS.WikiSystem.Repository.Interfaces.Mappers
{
    public interface IDocumentTagRepository
    {
        Task<bool> AddIfNotExistsAsync(int primaryId, int secondaryId);
        Task CreateMappingIfNotExistsAsync(DocumentTag documentTag);
        Task<bool> RemoveAsync(int primaryId, int secondaryId);
    }
}