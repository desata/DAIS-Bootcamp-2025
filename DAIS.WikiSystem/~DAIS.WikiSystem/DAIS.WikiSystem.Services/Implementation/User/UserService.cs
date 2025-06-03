using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository.Interfaces.User;
using DAIS.WikiSystem.Services.DTOs.User;
using DAIS.WikiSystem.Services.Interfaces.User;

namespace DAIS.WikiSystem.Services.Implementation.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserResponse> GetByIdAsync(int userId)
        {
            var user = await _userRepository.RetrieveAsync(userId);
            return (GetUserResponse)MapToUserInfo(user);
        }

        private UserInfo MapToUserInfo(Models.User user)
        {
            return new UserInfo
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = (Role)user.Role,
                AccessLevel = (AccessLevel)user.AccessLevel,
            };
        }
    }
}