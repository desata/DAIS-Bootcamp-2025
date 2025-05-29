using System.ComponentModel.DataAnnotations;

namespace OfficeResourcesReservationSystem.Models
{
    public class ResourceReservation
    {

        [Required]
        public int ReservationId { get; set; }
        [Required]
        public int ResourceId { get; set; }
    }
}
