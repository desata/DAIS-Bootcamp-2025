using DAIS.WikiSystem.Services.DTOs.Tag;

namespace DAIS.WikiSystem.Services.Interfaces.Tag
{
    public interface ITagService
    {
        Task<GetTagResponse> GetByIdAsync(int tagId);
        Task<GetAllTagsResponse> GetAllAsync();
    }
}
