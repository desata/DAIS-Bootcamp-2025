using Exam.Models;

namespace Exam.Repository.Interfaces
{
    public interface IReservationRepository
    {
        Task<Reservation?> RetrieveByIdAsync(int reservationId);
        Task<List<Reservation>> RetrieveByUserIdAsync(int userId);
        Task<int> CreateAsync(Reservation reservation);
        Task<bool> DeleteAsync(int reservationId);

        // TODO: Check if this method is needed
        Task<List<Reservation>> RetrieveCollectionAsync();

        // Note: The following methods are not part of the original code but are included for completeness.
        Task<bool> UpdateAsync(Reservation reservation);
    }
}
