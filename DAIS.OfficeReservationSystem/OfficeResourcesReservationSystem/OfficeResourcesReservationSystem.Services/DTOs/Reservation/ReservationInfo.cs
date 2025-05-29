using System.ComponentModel.DataAnnotations;

namespace OfficeResourcesReservationSystem.Services.DTOs.Reservation
{
    public class ReservationInfo
    {
        public int ReservationId { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }`
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public required string Purpose { get; set; }
        public int NumberOfParticipants { get; set; }
        public bool IsActive { get; set; }
    }
}
