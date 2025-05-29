using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.Employee
{
    public class EmployeeUpdate
    {
        public SqlString? FullName { get; set; }

        public SqlString? PasswordHash { get; set; }
    }
}