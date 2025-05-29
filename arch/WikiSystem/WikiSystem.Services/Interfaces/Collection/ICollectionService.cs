using WikiSystem.Services.DTOs.Collection;

namespace WikiSystem.Services.Interfaces.Collection
{
    public interface ICollectionService
    {
        Task<GetCollectionByCreatorResponse> GetByCreatorIdAsync(int creatorId);
        Task<CreateCollectionResponse> CreateCollectionAsync(CreateCollectionRequest request);
    }
}
