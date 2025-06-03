using DAIS.WikiSystem.Repository.Base;

namespace DAIS.WikiSystem.Repository.Interfaces.Category
{
    public interface ICategoryRepository : IBaseRepository<Models.Category, CategoryFilter, CategoryUpdate>
    {
    }
}