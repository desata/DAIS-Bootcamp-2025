using BirthdayGifts.Models;
using BirthdayGifts.Repository.Base;
using BirthdayGifts.Repository.Helpers;
using BirthdayGifts.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BirthdayGifts.Repository.Implementations
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration)
            : base(configuration, "Employee")
        {
        }

        protected override string[] GetColumns()
        {
            return new[]
            {
                "EmployeeId",
                "Name",
                "DateOfBirth",
                "Username",
                "Password"
            };
        }

        protected override Employee MapToEntity(SqlDataReader reader)
        {
            return new Employee
            {
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                Password = reader.GetString(reader.GetOrdinal("Password"))
            };
        }

        protected override Dictionary<string, object> MapToParameters(Employee entity)
        {
            return new Dictionary<string, object>
            {
                { "Name", entity.Name },
                { "DateOfBirth", entity.DateOfBirth },
                { "Username", entity.Username },
                { "Password", entity.Password }
            };
        }

        public async Task<Employee> GetByUsername(string username)
        {
            var filter = new Filter();
            filter.Condition("Username", username);
            var employees = await ReceiveCollection(filter);
            return employees.FirstOrDefault();
        }

    }
}