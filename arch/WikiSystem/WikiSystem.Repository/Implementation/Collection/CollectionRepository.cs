using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.Collection;

namespace WikiSystem.Repository.Implementation.Collection
{
    public class CollectionRepository : BaseMapperRepository<Models.Collection>, ICollectionRepository
    {
        private const string IdDbFieldEnumeratorName = "CollectionId";
        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Name",
            "CreatorId"
        };
        protected override string GetTableName()
        {
            return "Collections";
        }
        protected override Models.Collection MapEntity(SqlDataReader reader)
        {
            return new Models.Collection
            {
                CollectionId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                CreatorId = Convert.ToInt32(reader["CreatorId"])
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

        public Task<bool> UpdateAsync(int objectId, CollectionUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}

