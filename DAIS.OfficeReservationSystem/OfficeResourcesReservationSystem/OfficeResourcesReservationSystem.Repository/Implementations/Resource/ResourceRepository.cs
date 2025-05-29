using Microsoft.Data.SqlClient;
using OfficeResourcesReservationSystem.Repository.Base;
using OfficeResourcesReservationSystem.Repository.Helpers;
using OfficeResourcesReservationSystem.Repository.Interfaces.Resource;

namespace OfficeResourcesReservationSystem.Repository.Implementations.Resource
{
    public class ResourceRepository : BaseRepository<Models.Resource>, IResourceRepository
    {
        private const string IdDbFieldEnumeratorName = "ResourceId";
        protected override string GetTableName()
        {
            return "Resources";
        }
        protected override string[] GetColumns() => new string[]
        {
            IdDbFieldEnumeratorName,
            "Name",
            "Description",
            "IsAvailable",
            "ResourceTypeId"
        };
        protected override Models.Resource MapEntity(SqlDataReader reader)
        {
            return new Models.Resource
            {
                ResourceId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Name = Convert.ToString(reader["Name"]),
                Description = Convert.ToString(reader["Description"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                ResourceTypeId = Convert.ToInt32(reader["ResourceTypeId"])
            };
        }
        public Task<int> CreateAsync(Models.Resource entity)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Resource> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }
        public IAsyncEnumerable<Models.Resource> RetrieveCollectionAsync(ResourceFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Name is not null)
            {
                commandFilter.AddClause("Name", filter.Name);
            }
            if (filter.ResourceTypeId is not null)
            {
                commandFilter.AddClause("ResourceTypeId", filter.ResourceTypeId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ResourceUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                GetTableName(),
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Name", update.Name);
            updateCommand.AddSetClause("Description", update.Description);

            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}
