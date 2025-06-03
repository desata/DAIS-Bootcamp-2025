using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository.Base;
using DAIS.WikiSystem.Repository.Helpers;
using DAIS.WikiSystem.Repository.Interfaces.User;
using Microsoft.Data.SqlClient;

namespace DAIS.WikiSystem.Repository.Implementation.User
{
    public class UserRepository : BaseRepository<Models.User>, IUserRepository
    {
        private const string IdDbFieldEnumeratorName = "UserId";

        protected override string GetTableName() => "Users";

        protected override string[] GetColumns() => new[]
        {
            IdDbFieldEnumeratorName,
            "FirstName",
            "LastName",
            "Username",
            "Password",
            "Role",
            "AccessLevel"
        };

        protected override Models.User MapEntity(SqlDataReader reader)
        {
            return new Models.User
            {
                UserId = Convert.ToInt32(reader[IdDbFieldEnumeratorName]),
                FirstName = Convert.ToString(reader["FirstName"]),
                LastName = Convert.ToString(reader["LastName"]),
                Username = Convert.ToString(reader["Username"]),
                Password = Convert.ToString(reader["Password"]),
                Role = (Role)Convert.ToInt32(reader["Role"]),
                AccessLevel = (AccessLevel)Convert.ToInt32(reader["AccessLevel"])
            };
        }
        public Task<int> CreateAsync(Models.User entity)
        {
            throw new NotImplementedException();
        }

        public Task<Models.User> RetrieveAsync(int objectId)
        {
            return base.RetrieveAsync(IdDbFieldEnumeratorName, objectId);
        }

        public IAsyncEnumerable<Models.User> RetrieveCollectionAsync(UserFilter filter)
        {
            Filter commandFilter = new Filter();

            if (filter.Username is not null)
            {
                commandFilter.AddCondition("Username", filter.Username);
            }

            return base.RetrieveCollectionAsync(commandFilter);
        }

        public Task<bool> UpdateAsync(int objectId, UserUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int objectId)
        {
            throw new NotImplementedException();
        }

    }
}
