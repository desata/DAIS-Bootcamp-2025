using Exam.Models;
using Exam.Repository.Interfaces;
using Microsoft.Data.SqlClient;

namespace Exam.Repository.Implementation
{
    public class ReservationRepository : IReservationRepository
    {


        public async Task<int> CreateAsync(Reservation reservation)
        {
            try
            {
                using var connection = await ConnectionFactory.CreateConnectionAsync();
                using var command = new SqlCommand(@"
        INSERT INTO Reservations (ReservationDate, WorkplaceId, UserId)
        OUTPUT INSERTED.ReservationId
        VALUES (@ReservationDate, @WorkplaceId, @UserId)", connection);

                command.Parameters.AddWithValue("@ReservationDate", reservation.ReservationDate);
                command.Parameters.AddWithValue("@WorkplaceId", reservation.WorkplaceId);
                command.Parameters.AddWithValue("@UserId", reservation.UserId);

                var insertedId = await command.ExecuteScalarAsync();
                return Convert.ToInt32(insertedId);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error while creating reservation", ex);
            }
        }

        public async Task<bool> DeleteAsync(int reservationId)
        {
            try
            {
                using var connection = await ConnectionFactory.CreateConnectionAsync();
                using var command = new SqlCommand(@"
            DELETE FROM Reservations
            WHERE ReservationId = @ReservationId", connection);

                command.Parameters.AddWithValue("@ReservationId", reservationId);

                var affectedRows = await command.ExecuteNonQueryAsync();
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error deleting reservation", ex);
            }
        }

        public async Task<Reservation?> RetrieveByIdAsync(int reservationId)
        {
            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Reservations WHERE ReservationId = @reservationId", connection);
            command.Parameters.AddWithValue("@ReservationId", reservationId);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return MapReservation(reader);
            }

            return null;
        }

        public async Task<List<Reservation>> RetrieveByUserIdAsync(int userId)
        {
            var reservations = new List<Reservation>();

            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Reservations WHERE UserId = @userId", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reservations.Add(MapReservation(reader));
            }

            return reservations;
        }

        public async Task<List<Reservation>> RetrieveCollectionAsync()
        {
            // TODO Check if this method is needed
            var reservations = new List<Reservation>();

            using var connection = await ConnectionFactory.CreateConnectionAsync();
            using var command = new SqlCommand("SELECT * FROM Favorites", connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                reservations.Add(MapReservation(reader));
            }

            return reservations;
        }

        public Task<bool> UpdateAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        private static Reservation MapReservation(SqlDataReader reader)
        {
            return new Reservation
            {
                ReservationId = Convert.ToInt32(reader["ReservationId"]),
                ReservationDate = Convert.ToDateTime(reader["ReservationDate"]),
                UserId = Convert.ToInt32(reader["UserId"]),
                WorkplaceId = Convert.ToInt32(reader["WorkplaceId"])

            };
        }
    }
}
