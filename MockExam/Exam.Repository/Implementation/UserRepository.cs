using Exam.Models;
using Exam.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Exam.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        public Task<int> CreateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<User>> RetrieveCollectionAsync()
        {
            var users = new List<User>();

            using var connection = await ConnectionFactory.CreateConnectionAsync();
            var command = new SqlCommand("SELECT * FROM Users", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                users.Add(MapUser(reader));
            }

            return users;
        }

        public async Task<User?> RetrieveByIdAsync(int userId)
        {
            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Users WHERE UserId = @userId", connection);
            command.Parameters.AddWithValue("@UserId", userId);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapUser(reader);
            }

            return null;

        }

        public async Task<User?> RetrieveByUsernameAsync(string username)
        {
            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Users WHERE Username = @Username", connection);
            command.Parameters.AddWithValue("@Username", username);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapUser(reader);
            }

            return null;
        }

        public Task<bool> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }


        private static User MapUser(SqlDataReader reader)
        {
            return new User
            {
                UserId = Convert.ToInt32(reader["UserId"]),
                Name = Convert.ToString(reader["Name"]),
                Email = Convert.ToString(reader["Email"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"])
            };
        }
    }
}
