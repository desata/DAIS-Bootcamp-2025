using Exam.Repository.Interfaces;
using Exam.Services.DTOs.Authentication;
using Exam.Services.Helpers;
using Exam.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Services.Implementation
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

            var user = await _userRepository.RetrieveByUsernameAsync(request.Username);
          
            if (user is null || user.Password != hashedPassword)
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
                Name = user.Name,
                Email = user.Email,
            };
        }
    }
}
