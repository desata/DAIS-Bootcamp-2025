using DAIS.WikiSystem.Repository.Interfaces;
using DAIS.WikiSystem.Repository.Interfaces.Category;
using DAIS.WikiSystem.Services.DTOs.Category;
using DAIS.WikiSystem.Services.Interfaces.Category;

namespace DAIS.WikiSystem.Services.Implementation.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<GetAllCategoriesResponse> GetAllAsync()
        {
            var categories = await _categoryRepository.RetrieveCollectionAsync(new CategoryFilter()).ToListAsync();
            var allCategoriesResponse = new GetAllCategoriesResponse
            {
                Categories = categories.Select(MapToCategoryInfo).ToList(),
                TotalCount = categories.Count
            };
            return allCategoriesResponse;
        }

        public async Task<GetCategoryResponse> GetByIdAsync(int categoryid)
        {
            var category = await _categoryRepository.RetrieveAsync(categoryid);
            return (GetCategoryResponse)MapToCategoryInfo(category);
        }

        private CategoryInfo MapToCategoryInfo(Models.Category category)
        {
            return new CategoryInfo
            {
                CategoryId = category.CategoryId,
                Name = category.Name
            };
        }

    }
}
