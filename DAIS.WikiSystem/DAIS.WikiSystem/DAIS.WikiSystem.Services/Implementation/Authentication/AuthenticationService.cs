using DAIS.WikiSystem.Repository.Interfaces.User;
using DAIS.WikiSystem.Services.DTOs.Authentication;
using DAIS.WikiSystem.Services.Helpers;
using DAIS.WikiSystem.Services.Interfaces.Authentication;
using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Services.Implementation.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Username and password are required"
                };
            }

            var hashedPassword = SecurityHelper.HashPassword(request.Password);
            var filter = new UserFilter { Username = new SqlString(request.Username) };

            var users = await _userRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var user = users.SingleOrDefault();

            if (user == null || user.Password != hashedPassword)
            {
                return new LoginResponse
                {
                    IsSuccess = false,
                    ErrorMessage = "Invalid username or password"
                };
            }

            return new LoginResponse
            {
                IsSuccess = true,
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                AccessLevel = user.AccessLevel,
                Role = user.Role
            };
        }
    }
}
