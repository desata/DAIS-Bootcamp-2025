using Microsoft.Data.SqlClient;
using OfficeResourcesReservationSystem.Repository.Base;
using OfficeResourcesReservationSystem.Repository.Helpers;
using OfficeResourcesReservationSystem.Repository.Interfaces.Employee;

namespace OfficeResourcesReservationSystem.Repository.Implementations.Employee
{
    public class EmployeeRepository : BaseRepository<Models.Employee>, IEmployeeRepository
    {
        private const string IdDbFieldEnumeratorName = "EmployeeId";
        protected override string GetTableName()
        {
            return "Employees";
        }
        protected override string[] GetColumns() => new string[]
        {
            IdDbFieldEnumeratorName,
            "FullName",
            "Username",
            "PasswordHash"
        };
        protected override Models.Employee MapEntity(SqlDataReader reader)
        {
            return new Models.Employee
            {
                EmployeeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                FullName = Convert.ToString(reader["FullName"]),
                Username = Convert.ToString(reader["Username"]),
                PasswordHash = Convert.ToString(reader["PasswordHash"])
            };
        }

        public Task<int> CreateAsync(Models.Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employee> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Employee> RetrieveCollectionAsync(EmployeeFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Username is not null)
            {
                commandFilter.AddClause("Username", filter.Username);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, EmployeeUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(connection, GetTableName(), IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("FullName", update.FullName);
            updateCommand.AddSetClause("PasswordHash", update.PasswordHash);


            return await updateCommand.ExecuteNonQueryAsync() == 1;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}