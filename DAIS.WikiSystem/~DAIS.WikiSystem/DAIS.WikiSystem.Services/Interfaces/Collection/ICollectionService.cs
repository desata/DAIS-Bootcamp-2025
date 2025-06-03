using DAIS.WikiSystem.Services.DTOs.Collection;

namespace DAIS.WikiSystem.Services.Interfaces.Collection
{
    public interface ICollectionService
    {
        Task<GetCollectionResponse> GetByIdAsync(int collectionId);
        Task<GetAllCollectionByCreatorIdResponse> GetAllByCreatorIdAsync(int creatorId);
        Task<CreateCollectionResponse> CreateCollectionAsync(CreateCollectionRequest request);
        Task AddDocumentsToCollectionAsync(AddDocumentsToCollectionRequest request);
    }
}
