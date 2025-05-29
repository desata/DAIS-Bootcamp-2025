using System.Data.SqlTypes;
using WikiSystem.Repository.Interfaces.Employee;
using WikiSystem.Services.DTOs.Authentication;
using WikiSystem.Services.Helpers;
using WikiSystem.Services.Interfaces.Authentication;

namespace WikiSystem.Services.Implementation.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public AuthenticationService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.PasswordHash))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Username and password are required"
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.PasswordHash);
            var filter = new EmployeeFilter { Username = new SqlString(request.Username) };

            var employees = await _employeeRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var employee = employees.SingleOrDefault();

            if (employee == null || employee.PasswordHash != hashedPassword)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            return new LoginResponse
            {
                Success = true,
                EmployeeId = employee.EmployeeId,
                FullName = employee.FullName,
                Role = employee.Role,
                AccessLevel = employee.AccessLevel
            };
        }
    }
}
