using System.Data.SqlTypes;

namespace WikiSystem.Repository.Interfaces.Employee
{
    public class EmployeeFilter
    {
        public SqlString? Username { get; set; }
        public SqlString? FullName { get; set; }
    }
}