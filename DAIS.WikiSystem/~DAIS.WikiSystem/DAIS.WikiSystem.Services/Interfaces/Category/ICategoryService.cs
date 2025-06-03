using DAIS.WikiSystem.Services.DTOs.Category;

namespace DAIS.WikiSystem.Services.Interfaces.Category
{
    public interface ICategoryService
    {
        Task<GetCategoryResponse> GetByIdAsync(int categoryId);
        Task<GetAllCategoriesResponse> GetAllAsync();
    }
}
