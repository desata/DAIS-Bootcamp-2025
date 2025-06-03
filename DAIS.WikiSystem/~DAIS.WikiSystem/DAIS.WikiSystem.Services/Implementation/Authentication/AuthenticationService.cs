using DAIS.WikiSystem.Models.Enums;
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
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
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
            var filter = new UserFilter { Username = new SqlString(request.Username) };

            var users = await _userRepository.RetrieveCollectionAsync(filter).ToListAsync();
            var user = users.SingleOrDefault();

            if (user == null || user.Password != hashedPassword)
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
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = (Role)user.Role,
                AccessLevel = (AccessLevel)user.AccessLevel,

            };
        }
    }
}