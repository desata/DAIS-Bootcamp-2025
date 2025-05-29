using Wiki.Repository.Helpers;

namespace Wiki.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> Create(T entity);

        Task<T> RetrieveById(int objectId);

        Task<IEnumerable<T>> RetrieveCollection(Filter filter);

        Task<bool> Update(int objectId, Update update);

        Task<bool> Delete(int objectId);
    }
}