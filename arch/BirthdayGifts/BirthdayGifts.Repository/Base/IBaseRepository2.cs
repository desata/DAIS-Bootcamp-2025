using BirthdayGifts.Repository.Helpers;
using System.Runtime.CompilerServices;

namespace BirthdayGifts.Repository.Base2
{
    public interface IBaseRepository2<TObj, TFilter, TUpdate>
        where TObj : class
    {
        Task<int> CreateAsync(TObj entity);
        Task<TObj> RetrieveAsync(int objectId);
        IAsyncEnumerable<TObj> RetrieveCollectionAsync(TFilter filter);
        Task<bool> UpdateAsync(int objectId, TUpdate update);
        Task<bool> DeleteAsync(int objectId);
    }
}