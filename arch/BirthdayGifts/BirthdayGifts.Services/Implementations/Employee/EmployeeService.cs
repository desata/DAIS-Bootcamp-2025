using BirthdayGifts.Repository.Interfaces.Employee;
using BirthdayGifts.Services.DTOs.Employee;
using BirthdayGifts.Services.Helpers;
using BirthdayGifts.Services.Interfaces.Employee;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace BirthdayGifts.Services.Implementations.Employee
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository2 _employeeRepository;

        public EmployeeService(IEmployeeRepository2 employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<EmployeeDto> GetByIdAsync(int employeeId)
        {
            var employee = await _employeeRepository.RetrieveAsync(employeeId);
            return MapToDto(employee);
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.RetrieveCollectionAsync(new EmployeeFilter()).ToListAsync();
            return employees.Select(MapToDto);
        }

        public async Task<bool> UpdateFullNameAsync(int employeeId, string newFullName)
        {
            if (string.IsNullOrEmpty(newFullName))
            {
                throw new ValidationException("Full name cannot be empty");
            }

            var update = new EmployeeUpdate { FullName = new SqlString(newFullName) };
            return await _employeeRepository.UpdateAsync(employeeId, update);
        }

        public async Task<bool> UpdatePasswordAsync(int employeeId, string newPassword)
        {
            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ValidationException("Password cannot be empty");
            }

            var hashedPassword = SecurityHelper.HashPassword(newPassword);
            var update = new EmployeeUpdate { Password = new SqlString(hashedPassword) };
            return await _employeeRepository.UpdateAsync(employeeId, update);
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesWithUpcomingBirthdays(int daysAhead)
        {
            var allEmployees = await _employeeRepository.RetrieveCollectionAsync(new EmployeeFilter()).ToListAsync();

            var today = DateTime.Today;
            var upcomingBirthdays = allEmployees.Where(e =>
            {
                var nextBirthday = new DateTime(today.Year, e.BirthDate.Month, e.BirthDate.Day);
                if (nextBirthday < today)
                    nextBirthday = nextBirthday.AddYears(1);

                var daysUntilBirthday = (nextBirthday - today).Days;
                return daysUntilBirthday <= daysAhead;
            });

            return upcomingBirthdays.Select(MapToDto);
        }

        private EmployeeDto MapToDto(Models.Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Username = employee.Username,
                FullName = employee.FullName,
                BirthDate = employee.BirthDate
            };
        }

    }
}
