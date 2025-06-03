using DAIS.WikiSystem.Services.DTOs.Collection;

namespace DAIS.WikiSystem.Services.Interfaces.Collection
{
    public interface ICollectionService
    {
        Task<GetCollectionResponse> GetByIdAsync(int collectionId);
        Task<GetAllCollectionResponse> GetAllAsync();
        Task<GetAllCollectionResponse> GetAllByCreatorAsync(int? creatorId);
        Task<CreateCollectionResponse> CreateCollectionAsync(CreateCollectionRequest request);
        Task<AddDocumentsToCollectionResponse> AddDocumentsToCollectionAsync(AddDocumentsToCollectionRequest request);
    }
}