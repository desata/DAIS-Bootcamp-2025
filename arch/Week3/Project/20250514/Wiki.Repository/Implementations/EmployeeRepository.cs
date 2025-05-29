using Microsoft.Data.SqlClient;
using Wiki.Models;
using Wiki.Models.Enums;
using Wiki.Repository.Base;
using Wiki.Repository.Helpers;
using Wiki.Repository.Interfaces;

namespace Wiki.Repository.Implementations
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(ConnectionFactory connectionFactory)
            : base(connectionFactory, "Employee")
        {
        }


        protected override string[] GetColumns()
        {
            return new[]
            {
                "EmployeeId",
                "FullName",
                "Username",
                "PasswordHash",
                "Role",
                "AccessLevel"
            };
        }

        protected override Employee MapToEntity(SqlDataReader reader)
        {
            return new Employee
            {
                EmployeeId = reader.GetInt32(reader.GetOrdinal("EmployeeId")),
                FullName = reader.GetString(reader.GetOrdinal("FullName")),
                Username = reader.GetString(reader.GetOrdinal("Username")),
                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                Role = (Role)reader.GetInt32(reader.GetOrdinal("Role")),
                AccessLevel = (AccessLevel)reader.GetInt32(reader.GetOrdinal("AccessLevel"))
            };
        }

        protected override Dictionary<string, object> MapToParameters(Employee employee)
        {
            return new Dictionary<string, object>
            {
                { "FullName", employee.FullName },
                { "Username", employee.Username },
                { "PasswordHash", employee.PasswordHash },
                { "Role", (int)employee.Role },
                { "AccessLevel", (int)employee.AccessLevel }
            };
        }

        protected override object GetValueFromEntity(Employee employee, string columnName)
        {
            return columnName switch
            {
                "FullName" => employee.FullName,
                "Username" => employee.Username,           
                "PasswordHash" => employee.PasswordHash,
                "Role" => (int)employee.Role,            
                "AccessLevel" => (int)employee.AccessLevel,
                _ => throw new ArgumentException($"Unknown column: {columnName}")
            };
        }

    }
}
