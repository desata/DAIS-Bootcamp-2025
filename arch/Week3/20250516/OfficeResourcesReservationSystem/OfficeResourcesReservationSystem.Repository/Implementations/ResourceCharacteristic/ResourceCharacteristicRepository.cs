using Microsoft.Data.SqlClient;
using OfficeResourcesReservationSystem.Repository.Base;
using OfficeResourcesReservationSystem.Repository.Helpers;
using OfficeResourcesReservationSystem.Repository.Interfaces.ResourceCharacteristic;

namespace OfficeResourcesReservationSystem.Repository.Implementations.ResourceCharacteristic
{
    public class ResourceCharacteristicRepository : BaseRepository<Models.ResourceCharacteristic>, IResourceCharacteristicRepository
    {
        private const string IdDbFieldEnumeratorName = "ResourceCharacteristicId";

        protected override string GetTableName()
        {
            return "ResourceCharacteristics";
        }

        protected override string[] GetColumns()
        {
            return new string[]
            {
                IdDbFieldEnumeratorName,
                "Name",
                "Value",
                "ResourceId"
            };
        }

        protected override Models.ResourceCharacteristic MapEntity(SqlDataReader reader)
        {
            return new Models.ResourceCharacteristic
            {
                ResourceCharacteristicId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                Value = Convert.ToString(reader["Value"]),
                ResourceId = Convert.ToInt32(reader["ResourceId"])
            };

        }

        public Task<int> CreateAsync(Models.ResourceCharacteristic entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<Models.ResourceCharacteristic> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.ResourceCharacteristic> RetrieveCollectionAsync(ResourceCharacteristicFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddClause("Name", filter.Name);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ResourceCharacteristicUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                GetTableName(),
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);
            updateCommand.AddSetClause("Value", update.Value);

            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }
        public Task<bool> DeleteAsync(int objectId)
        {
            return base.DeleteAsync(IdDbFieldEnumeratorName, objectId);
        }

    }
}
