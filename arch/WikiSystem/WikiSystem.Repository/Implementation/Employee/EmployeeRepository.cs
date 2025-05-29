using Microsoft.Data.SqlClient;
using WikiSystem.Models.Enums;
using WikiSystem.Repository.Base;
using WikiSystem.Repository.Helpers;
using WikiSystem.Repository.Interfaces.Employee;

namespace WikiSystem.Repository.Implementation.Employee
{
    public class EmployeeRepository : BaseMapperRepository<Models.Employee>, IEmployeeRepository
    {
        private const string IdDbFieldEnumeratorName = "EmployeeId";


        protected override string GetTableName()
        {
            return "Employees";
        }
        protected override string[] GetColumns() => new[]
         {
                IdDbFieldEnumeratorName,
                "FullName",
                "Username",
                "PasswordHash",
                "Role",
                "AccessLevel"
         };

        protected override Models.Employee MapEntity(SqlDataReader reader)
        {
            return new Models.Employee
            {
                EmployeeId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                FullName = Convert.ToString(reader["FullName"]),
                Username = Convert.ToString(reader["Username"]),
                PasswordHash = Convert.ToString(reader["PasswordHash"]),
                Role = (Role)Convert.ToInt32(reader["Role"]),
                AccessLevel = (AccessLevel)Convert.ToInt32(reader["AccessLevel"])

            };
        }

        public Task<int> CreateAsync(Models.Employee entity)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employee> RetrieveAsync(int objectId)
        {
            return RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.Employee> RetrieveCollectionAsync(EmployeeFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.FullName is not null)
            {
                commandFilter.AddCondition("FullName", filter.FullName);
            }
            if (filter.Username is not null)
            {
                commandFilter.AddCondition("Username", filter.Username);
            }

            return RetrieveCollectionAsync(commandFilter);
        }

        public async Task<bool> UpdateAsync(int objectId, EmployeeUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }
    }
}