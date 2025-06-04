using Exam.Services.DTOs.Reservation;

namespace Exam.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public WorkplaceListViewModel AvailableWorkplaces { get; set; }
        public List<ReservationInfo> UserReservations { get; set; } = new();
    }
}
