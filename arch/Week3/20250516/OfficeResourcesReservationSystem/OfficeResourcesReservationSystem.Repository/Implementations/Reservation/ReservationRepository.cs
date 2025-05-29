using Microsoft.Data.SqlClient;
using OfficeResourcesReservationSystem.Repository.Base;
using OfficeResourcesReservationSystem.Repository.Helpers;
using OfficeResourcesReservationSystem.Repository.Interfaces.Reservation;

namespace OfficeResourcesReservationSystem.Repository.Implementations.Reservation
{
    public class ReservationRepository : BaseRepository<Models.Reservation>, IReservationRepository
    {
        private const string IdDbFieldEnumeratorName = "ReservationId";
        protected override string GetTableName()
        {
            return "Reservations";
        }

        protected override string[] GetColumns() => new string[]
        {
            IdDbFieldEnumeratorName,
            "CreatorId",
            "StartDate",
            "EndDate",
            "Purpose",
            "NumberOfParticipants",
            "IsActive"
        };


        protected override Models.Reservation MapEntity(SqlDataReader reader)
        {
            return new Models.Reservation
            {
                ReservationId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                CreatorId = Convert.ToInt32(reader["CreatorId"]),
                StartDate = Convert.ToDateTime(reader["StartDate"]),
                EndDate = Convert.ToDateTime(reader["EndDate"]),
                Purpose = Convert.ToString(reader["Purpose"]),
                NumberOfParticipants = Convert.ToInt32(reader["NumberOfParticipants"]),
                IsActive = Convert.ToBoolean(reader["IsActive"])
            };
        }
        public Task<int> CreateAsync(Models.Reservation entity)
        {
            return base.CreateAsync(entity, IdDbFieldEnumeratorName);
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Reservation> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Reservation> RetrieveCollectionAsync(ReservationFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.CreatorId is not null)
            {
                commandFilter.AddClause("CreatorId", filter.CreatorId);
            }
            if (filter.StartDate is not null)
            {
                commandFilter.AddClause("StartDate", filter.StartDate);
            }
            if (filter.IsActive is not null)
            {
                commandFilter.AddClause("IsActive", filter.IsActive);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, ReservationUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("StartDate", update.StartDate);
            updateCommand.AddSetClause("EndDate", update.EndDate);
            updateCommand.AddSetClause("Purpose", update.Purpose);
            updateCommand.AddSetClause("NumberOfParticipants", update.NumberOfParticipants);
            updateCommand.AddSetClause("IsActive", update.IsActive);

            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }


    }
}
