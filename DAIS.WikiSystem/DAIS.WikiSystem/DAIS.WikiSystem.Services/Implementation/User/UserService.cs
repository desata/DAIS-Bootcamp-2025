using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Services.DTOs.User;

namespace DAIS.WikiSystem.Services.Implementation.User
{
    public class UserService : Interfaces.User.IUserService
    {
        private readonly Repository.Interfaces.User.IUserRepository _userRepository;

        public UserService(Repository.Interfaces.User.IUserRepository userRepository)
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