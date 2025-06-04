using Exam.Models;
using Exam.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using System.Net.NetworkInformation;

namespace Exam.Repository.Implementation
{
    public class FavoriteRepository : IFavoriteRepository

    {
        public async Task<int> CreateAsync(Favorite favorite)
        {
            try
            {
                using var connection = await ConnectionFactory.CreateConnectionAsync();
                using var command = new SqlCommand(@"
            INSERT INTO Favorites (Name, WorkplaceId, UserId)
            OUTPUT INSERTED.FavoriteId
            VALUES (@Name, @WorkplaceId, @UserId)", connection);

                command.Parameters.AddWithValue("@Name", favorite.Name);
                command.Parameters.AddWithValue("@WorkplaceId", favorite.WorkplaceId);
                command.Parameters.AddWithValue("@UserId", favorite.UserId);

                var insertedId = await command.ExecuteScalarAsync();
                return Convert.ToInt32(insertedId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while creating favorite", ex);
            }
        }

        public async Task<bool> DeleteAsync(int favoriteId)
        {
            try
            {
                using var connection = await ConnectionFactory.CreateConnectionAsync();
                using var command = new SqlCommand(@"
            DELETE FROM Favorites
            WHERE FavoriteId = @FavoriteId", connection);

                command.Parameters.AddWithValue("@FavoriteId", favoriteId);

                var affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting favorite", ex);
            }
        }

        public async Task<Favorite?> RetrieveByIdAsync(int favoriteId)
        {       
            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Favorites WHERE FavoriteId = @favoriteId", connection);
            command.Parameters.AddWithValue("@FavoriteId", favoriteId);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapFavorite(reader);
            }

            return null;
        }

        public async Task<List<Favorite>> RetrieveByUserIdAsync(int userId)
        {
            var favorites = new List<Favorite>();

            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Favorites WHERE UserId = @userId", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                favorites.Add(MapFavorite(reader));
            }

            return favorites;
        }

        public async Task<List<Favorite>> RetrieveCollectionAsync()
        {
            // TODO Check if this method is needed
            var favorites = new List<Favorite>();

            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Favorites", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                favorites.Add(MapFavorite(reader));
            }

            return favorites;
        }

        public Task<bool> UpdateAsync(Favorite favorite)
        {
            throw new NotImplementedException();
        }

        private static Favorite MapFavorite(SqlDataReader reader)
        {
            return new Favorite
            {
                FavoriteId = Convert.ToInt32(reader["FavoriteId"]),
                Name = Convert.ToString(reader["Name"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                WorkplaceId = Convert.ToInt32(reader["WorkplaceId"])

            };
        }
    }
}
