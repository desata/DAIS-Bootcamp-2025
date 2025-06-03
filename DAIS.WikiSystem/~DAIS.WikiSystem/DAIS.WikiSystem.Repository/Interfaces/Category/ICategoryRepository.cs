using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Interfaces.Category;

namespace DAIS.WikiSystem.Repository.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Models.Category, CategoryFilter, CategoryUpdate>
    {
    }
}