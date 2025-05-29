using BirthdayGifts.Repository.Interfaces.Employee;
using BirthdayGifts.Services.DTOs.Authentication;
using BirthdayGifts.Services.Helpers;
using BirthdayGifts.Services.Interfaces.Authentication;
using System.Data.SqlTypes;

namespace BirthdayGifts.Services.Implementations.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository2 _employeeRepository;

        public AuthenticationService(IEmployeeRepository2 employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Username and password are required"
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new EmployeeFilter { Username = new SqlString(request.Username) };

            var employees = await _employeeRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var employee = employees.FirstOrDefault();

            if (employee == null || employee.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid username or password"
                };
            }

            return new LoginResponse
            {
                Success = true,
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName
            };
        }
    }
}
