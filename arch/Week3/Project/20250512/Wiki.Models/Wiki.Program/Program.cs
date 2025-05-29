using System.Security.Cryptography;
using System.Text;
using System;
using Wiki.Models;
using Microsoft.Data.SqlClient;
using Wiki.Models.Enums;

namespace Wiki.Program
{
    public class AuthService
    {

        private readonly string _connectionString;

        public AuthService(string connectionString)
        {
            _connectionString = connectionString;
        }

public Employee Login(string username, string password)
        {
            string hashedPassword = ComputeSha256Hash(password);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = @"SELECT EmployeeId, FullName, Username, Role, AccessLevel
                             FROM Employees
                             WHERE Username = @Username AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", hashedPassword);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            Console.WriteLine("Success, yay");

                            return new Employee
                            {
                                EmployeeId = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Username = reader.GetString(2),
                                PasswordHash = reader.GetString(3),
                                Role = (Role)reader.GetInt32(4),
                                AccessLevel = (AccessLevel)reader.GetInt32(5)
                            };
                        }
                    }
                }
            }

            return null; // Invalid login
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
