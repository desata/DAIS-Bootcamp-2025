using WikiSystem.Services.DTOs.Category;

namespace WikiSystem.Services.Interfaces.Category
{
    public interface ICategoryService
    {
        Task<GetCategoryResponse> GetByIdAsync(int categoryid);
        Task<GetAllCategoriesResponse> GetAllAsync();
    }
}
