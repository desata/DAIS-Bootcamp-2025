using DAIS.WikiSystem.Repository.Base;

namespace DAIS.WikiSystem.Repository.Interfaces.Collection
{
    public interface ICollectionRepository : IBaseRepository<Models.Collection, CollectionFilter, CollectionUpdate>
    {
    }
}
