using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.Employee
{
    public class EmployeeUpdate
    {
        public SqlString? FullName { get; set; }

        public SqlString? Password { get; set; }
    }
}