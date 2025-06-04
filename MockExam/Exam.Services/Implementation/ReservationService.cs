using Exam.Models;
using Exam.Repository.Implementation;
using Exam.Repository.Interfaces;
using Exam.Services.DTOs.Reservation;
using Exam.Services.Interfaces;

namespace Exam.Services.Implementation
{
    public class ReservationService : IReservationService
    {

        private readonly IReservationRepository _reservationRepository;
        private readonly IWorkplaceRepository _workplaceRepository;

        public ReservationService(IReservationRepository reservationRepository, IWorkplaceRepository workplaceRepository)
        {
            _reservationRepository = reservationRepository;
            _workplaceRepository = workplaceRepository;
        }

        public async Task<CreateReservationResponse> CreateReservationAsync(CreateReservationRequest request)
        {
            var response = new CreateReservationResponse();

            if (request.ReservationDate.Date < DateTime.Today)
            {
                response.Success = false;
                response.ErrorMessage = "Reservations for past dates are not allowed.";
                return response;
            }

            if (request.ReservationDate.Date > DateTime.Today.AddDays(14))
            {
                response.Success = false;
                response.ErrorMessage = "Reservations cannot be made more than 2 weeks in advance.";
                return response;
            }

            var userReservations = await _reservationRepository.RetrieveByUserIdAsync(request.UserId);
            if (userReservations.Any(r => r.ReservationDate.Date == request.ReservationDate.Date))
            {
                response.Success = false;
                response.ErrorMessage = "You already have a reservation for this date.";
                return response;
            }

            var reservation = new Reservation
            {
                ReservationDate = request.ReservationDate,
                UserId = request.UserId,
                WorkplaceId = request.WorkplaceId
            };

            var reservationId = await _reservationRepository.CreateAsync(reservation);

            response.Success = true;
            return response;
        }

        public async Task<DeleteReservationResponse> DeleteReservationAsync(DeleteReservationRequest request)
        {
            var response = new DeleteReservationResponse();

            var reservation = await _reservationRepository.RetrieveByIdAsync(request.ReservationId);

            if (reservation == null)
            {
                response.Success = false;
                response.ErrorMessage = "Reservation not found.";
                return response;
            }

            if (reservation.UserId != request.UserId)
            {
                response.Success = false;
                response.ErrorMessage = "You are not authorized to delete this reservation.";
                return response;
            }

            var workplace = await _workplaceRepository.RetrieveByIdAsync(reservation.WorkplaceId);

            var success = await _reservationRepository.DeleteAsync(request.ReservationId);

            response.Success = success;
            response.ErrorMessage = success ? "Reservation deleted successfully." : "Failed to delete reservation.";
            return response;
        }

        public async Task<List<ReservationInfo>> GetAllByUserIdAsync(int userId)
        {
            var reservations = await _reservationRepository.RetrieveByUserIdAsync(userId);
            var result = new List<ReservationInfo>();

            foreach (var r in reservations)
            {
                var workplace = await _workplaceRepository.RetrieveByIdAsync(r.WorkplaceId); 

                result.Add(new ReservationInfo
                {
                    ReservationId = r.ReservationId,
                    ReservationDate = r.ReservationDate,
                    WorkplaceId = r.WorkplaceId,
                    UserId = r.UserId,
                    Location = workplace.Location
                });
            }

            return result;
        }

        public async Task<ReservationInfo> GetByIdAsync(int reservationId)
        {
            var reservation = await _reservationRepository.RetrieveByIdAsync(reservationId);
            var workplace = await _workplaceRepository.RetrieveByIdAsync(reservation.WorkplaceId);

            if (reservation == null)
                return null;

            return new ReservationInfo
            {
                ReservationId = reservation.ReservationId,
                ReservationDate = reservation.ReservationDate,
                WorkplaceId = reservation.WorkplaceId,
                Location = workplace.Location,
                UserId = reservation.UserId
            };
        }
    }
}
