using Exam.Models;
using Exam.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Exam.Repository.Implementation
{
    public class WorkplaceRepository : IWorkplaceRepository
    {
        public Task<int> CreateAsync(Workplace workplace)
        {
            throw new NotImplementedException();
        }

        public async Task<Workplace?> RetrieveByIdAsync(int workplaceId)
        {
            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Workplaces WHERE WorkplaceId = @workplaceId", connection);
            command.Parameters.AddWithValue("@WorkplaceId", workplaceId);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapWorkplace(reader);
            }

            return null;
        }

        public async Task<List<Workplace>> RetrieveCollectionAsync()
        {
            var workplaces = new List<Workplace>();

            using var connection = await ConnectionFactory.CreateConnectionAsync();
            var command = new SqlCommand("SELECT * FROM Workplaces", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                workplaces.Add(MapWorkplace(reader));
            }

            return workplaces;
        }

        public async Task<bool> UpdateAsync(Workplace workplace)
        {
            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand(@"
        UPDATE Workplaces
        SET IsAvailable = @IsAvailable
        WHERE WorkplaceId = @WorkplaceId", connection);

            command.Parameters.AddWithValue("@IsAvailable", workplace.IsAvailable);
            command.Parameters.AddWithValue("@WorkplaceId", workplace.WorkplaceId);

            var affectedRows = await command.ExecuteNonQueryAsync();

            return affectedRows > 0;
        }

        public Task<bool> DeleteAsync(int workplaceId)
        {
            throw new NotImplementedException();
        }

        private static Workplace MapWorkplace(SqlDataReader reader)
        {
            return new Workplace
            {
                WorkplaceId = Convert.ToInt32(reader["WorkplaceId"]),
                HasMonitor = Convert.ToBoolean(reader["HasMonitor"]),
                HasDockingStation = Convert.ToBoolean(reader["HasDockingStation"]),
                HasWindow = Convert.ToBoolean(reader["HasWindow"]),
                HasPrinter = Convert.ToBoolean(reader["HasPrinter"]),
                IsAvailable = Convert.ToBoolean(reader["IsAvailable"]),
                Location = Convert.ToString(reader["Location"])
            };
        }
    }
}
