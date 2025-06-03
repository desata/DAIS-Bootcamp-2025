namespace DAIS.WikiSystem.Repository.Base
{
    public interface IBaseMapperRepository<TObj, TFilter, TUpdate>
        where TObj : class
    {
        Task<TObj> RetrieveAsync(int objectId);
        IAsyncEnumerable<TObj> RetrieveCollectionAsync(TFilter filter);
        Task<bool> AddIfNotExistsAsync(int primaryId, List<int> secondaryIds);
        Task<bool> CreateMappingIfNotExistsAsync(TObj entity);
        Task<bool> RemoveAsync(int primaryId, int secondaryId);
    }
}
