using WikiSystem.Models.Enums;

namespace WikiSystem.Services.DTOs.Employee
{
    public class EmployeeInfo
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
        public AccessLevel AccessLevel { get; set; }

    }
}
