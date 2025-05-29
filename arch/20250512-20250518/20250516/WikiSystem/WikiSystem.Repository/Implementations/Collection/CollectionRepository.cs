using Microsoft.Data.SqlClient;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.Collection;

namespace WikiSystem.Repository.Implementations.Collection
{
    public class CollectionRepository : BaseRepository<Models.Collection>, ICollectionRepository
    {

        private const string IdDbFieldEnumeratorName = "CollectionId";

        protected override string GetTableName()
        {
            return "Collections";
        }
        protected override string[] GetColumns()
        {
            return new string[]
            {
                "CollectionId",
                "Name",
                "CreatorId"
            };
        }
        protected override Models.Collection MapEntity(SqlDataReader reader)
        {
            return new Models.Collection
            {
                CollectionId = Convert.ToInt32(reader["CollectionId"]),
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
                commandFilter.AddClause("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, CollectionUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                GetTableName(),
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);

            return await updateCommand.ExecuteNonQueryAsync() == 1;

        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }


    }
}
