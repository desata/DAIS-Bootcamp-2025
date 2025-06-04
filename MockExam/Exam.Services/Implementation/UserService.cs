using Exam.Models;
using Exam.Repository.Interfaces;
using Exam.Services.DTOs.User;
using Exam.Services.Interfaces;

namespace Exam.Services.Implementation
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserInfo>> GetAllUsersAsync()
        {
            var users = await _userRepository.RetrieveCollectionAsync();

            return users.Select(user => new UserInfo
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email
            }).ToList();

           
        }

        public async Task<UserInfo?> GetByUsernameAsync(string username)
        {
            var user = await _userRepository.RetrieveByUsernameAsync(username);
            return new UserInfo
            {
                UserId = user.UserId,
                Name = user.Name,
                 Email = user.Email
            };

        }
    }
}