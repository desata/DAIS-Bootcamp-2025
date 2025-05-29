using Microsoft.Data.SqlClient;
using OfficeResourcesReservationSystem.Repository.Base;
using OfficeResourcesReservationSystem.Repository.Helpers;
using OfficeResourcesReservationSystem.Repository.Interfaces.ResourceType;

namespace OfficeResourcesReservationSystem.Repository.Implementations.ResourceType
{
    public class ResourceTypeRepository : BaseRepository<Models.ResourceType>, IResourceTypeRepository
    {
        private const string IdDbFieldEnumeratorName = "ResourceCharacteristicId";

        protected override string GetTableName()
        {
            return "ResourceTypes";
        }
        protected override string[] GetColumns() => new string[]
        {
            IdDbFieldEnumeratorName,
            "Name"
        };

        protected override Models.ResourceType MapEntity(SqlDataReader reader)
        {
            return new Models.ResourceType
            {
                ResourceTypeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"])
            };
        }

        public Task<int> CreateAsync(Models.ResourceType entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

        public Task<Models.ResourceType> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.ResourceType> RetrieveCollectionAsync(ResourceTypeFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddClause("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ResourceTypeUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                GetTableName(),
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);

            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }

    }
}
