using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.Category
{
    public interface ICategoryRepository : IBaseRepository<Models.Category, CategoryFilter, CategoryUpdate>
    {
    }
}