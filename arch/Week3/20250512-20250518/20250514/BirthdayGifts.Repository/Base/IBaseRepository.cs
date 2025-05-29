using BirthdayGifts.Repository.Helpers;


namespace BirthdayGifts.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<int> Create(T entity);

        Task<T> ReceiveById(int objectId);

        Task<IEnumerable<T>> ReceiveCollection(Filter filter);

        Task<bool> Update(int objectId, Update update);

        Task<bool> Delete(int objectId);
    }
}
