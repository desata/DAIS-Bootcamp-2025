using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "Reservation date is required")]
        public DateTime ReservationDate { get; set; }

        [Required(ErrorMessage = "Workplace is required")]
        public int WorkplaceId { get; set; }

        [Required(ErrorMessage = "User is required")]
        public int UserId { get; set; }

    }
}