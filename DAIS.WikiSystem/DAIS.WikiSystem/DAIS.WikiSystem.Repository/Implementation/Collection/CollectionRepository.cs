using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces.Collection;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.Collection
{
    public class CollectionRepository : BaseRepository<Models.Collection>, ICollectionRepository
    {
        private const string IdDbFieldEnumeratorName = "CollectionId";
        protected override string GetTableName() => "Collections";
        protected override string[] GetColumns() => new[] { IdDbFieldEnumeratorName, "Name", "CreatorId", "CreateDate" };

        protected override Models.Collection MapEntity(SqlDataReader reader)
        {
            return new Models.Collection
            {
                CollectionId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                CreatorId = Convert.ToInt32(reader["CreatorId"]),
                CreateDate = Convert.ToDateTime(reader["CreateDate"])

            };
        }

        public Task<int> CreateAsync(Models.Collection entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.Collection> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Collection> RetrieveCollectionAsync(CollectionFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddCondition("Name", filter.Name);
            }
            if (filter.CreatorId is not null)
            {
                commandFilter.AddCondition("CreatorId", filter.CreatorId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, CollectionUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
