using Microsoft.Data.SqlClient;
using OfficeResourcesReservationSystem.Repository.Base;
using OfficeResourcesReservationSystem.Repository.Helpers;
using OfficeResourcesReservationSystem.Repository.Interfaces.ResourceReservation;

namespace OfficeResourcesReservationSystem.Repository.Implementations.ResourceReservation
{
    public class ResourceReservationRepository : BaseRepository<Models.ResourceReservation>, IResourceReservationRepository
    {
        private const string IdDbFieldEnumeratorName = "ResourceReservationId";
        protected override string GetTableName()
        {
            return "ResourceReservations";
        }
        protected override string[] GetColumns() => new string[]
        {
            "ReservationId",
            "ResourceId"
    };
        protected override Models.ResourceReservation MapEntity(SqlDataReader reader)
        {
            return new Models.ResourceReservation
            {
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                ResourceId = Convert.ToInt32(reader["ResourceId"])
            };
        }
        public Task<int> CreateAsync(Models.ResourceReservation entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }
        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
        public Task<Models.ResourceReservation> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }
        public IAsyncEnumerable<Models.ResourceReservation> RetrieveCollectionAsync(ResourceReservationFilter filter)
        {
            Filter commandFilter = new Filter();
            if (filter.ReservationId is not null)
            {
                commandFilter.AddClause("ReservationId", filter.ReservationId);
            }            
            if (filter.ResourceId is not null)
            {
                commandFilter.AddClause("ResourceId", filter.ResourceId);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ResourceReservationUpdate update)
        {
            throw new NotImplementedException();
        }
    }
}
