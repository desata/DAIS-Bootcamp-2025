using System.ComponentModel.DataAnnotations;

namespace OfficeResourcesReservationSystem.Models
{
    public class Reservation
    {
        public int ReservationId { get; set; }

        [Required(ErrorMessage = "ResourceId is required")]
        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Purpose is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Purpose must be between 2 and 100 characters")]
        public required string Purpose { get; set; }

        [Required]
        public int NumberOfParticipants { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
