namespace Exam.Services.DTOs.Reservation
{
    public class CreateReservationRequest
    {
        public DateTime ReservationDate { get; set; }
        public int WorkplaceId { get; set; }
        public int UserId { get; set; }
    }
}
