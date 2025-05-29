using BirthdayGifts.Services.DTOs.Employee;

namespace BirthdayGifts.Services.Interfaces.Employee
{
    public interface IEmployeeService
    {
        Task<EmployeeDto> GetByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<bool> UpdateFullNameAsync(int employeeId, string newFullName);
        Task<bool> UpdatePasswordAsync(int employeeId, string newPassword);
        Task<IEnumerable<EmployeeDto>> GetEmployeesWithUpcomingBirthdays(int daysAhead);
    }

}
