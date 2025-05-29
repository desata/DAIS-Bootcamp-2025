using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.Employee
{
    public class EmployeeFilter
    {
        public SqlString? Username { get; set; }
    }
}