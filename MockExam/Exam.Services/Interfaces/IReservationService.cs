using Exam.Services.DTOs.Reservation;

namespace Exam.Services.Interfaces
{
    public interface IReservationService
    {
        Task<ReservationInfo> GetByIdAsync(int reservationId);
        Task<List<ReservationInfo>> GetAllByUserIdAsync(int userId);
        Task<CreateReservationResponse> CreateReservationAsync(CreateReservationRequest request);
        Task<DeleteReservationResponse> DeleteReservationAsync(DeleteReservationRequest request);

    }
}
