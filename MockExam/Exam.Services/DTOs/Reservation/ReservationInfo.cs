namespace Exam.Services.DTOs.Reservation
{
    public class ReservationInfo
    {
        public int ReservationId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int WorkplaceId { get; set; }
        public int UserId { get; set; }
    }
}
