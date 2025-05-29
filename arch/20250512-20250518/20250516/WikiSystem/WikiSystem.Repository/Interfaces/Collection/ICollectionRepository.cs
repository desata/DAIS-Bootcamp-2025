using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.Collection
{
    public interface ICollectionRepository : IBaseRepository<Models.Collection, CollectionFilter, CollectionUpdate>
    {
    }
}
