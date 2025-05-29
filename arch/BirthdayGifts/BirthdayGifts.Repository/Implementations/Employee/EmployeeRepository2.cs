using BirthdayGifts.Repository.Base2;
using BirthdayGifts.Repository.Helpers;
using BirthdayGifts.Repository.Interfaces.Employee;
using Microsoft.Data.SqlClient;

namespace BirthdayGifts.Repository.Implementations.Employee
{
    public class EmployeeRepository2 : BaseRepository2<Models.Employee>, IEmployeeRepository2
    {
        private const string IdDbFieldEnumeratorName = "EmployeeId";

        protected override string GetTableName()
        {
            return "Employees";
        }
        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "Username",
            "Password",
            "FullName",
            "BirthDate"
        };
        protected override Models.Employee MapEntity(SqlDataReader reader)
        {
            return new Models.Employee
            {
                EmployeeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"]),
                FullName = Convert.ToString(reader["FullName"]),
                BirthDate = Convert.ToDateTime(reader["BirthDate"])
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
                commandFilter.AddCondition("Username", filter.Username);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, EmployeeUpdate update)
        {
            using SqlConnection connection = await ConnectionFactory.CreateConnectionAsync();

            UpdateCommand updateCommand = new UpdateCommand(
                connection,
                "Employees",
                IdDbFieldEnumeratorName, objectId);

            updateCommand.AddSetClause("Password", update.Password);
            updateCommand.AddSetClause("FullName", update.FullName);

            return await updateCommand.ExecuteNonQueryAsync() > 0;
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }

}