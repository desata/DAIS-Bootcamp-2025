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

            var allCategories = new GetAllCategoriesResponse
            {
                Categories = categories.Select(MapToCategoryInfo).ToList(),
                Count = categories.Count
            };
            return allCategories;

        }

        public async Task<GetCategoryResponse> GetByIdAsync(int categoryId)
        {
            var category = await _categoryRepository.RetrieveAsync(categoryId);

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
