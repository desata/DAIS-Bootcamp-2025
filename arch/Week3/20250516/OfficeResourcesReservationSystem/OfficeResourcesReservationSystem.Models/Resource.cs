using System.ComponentModel.DataAnnotations;

namespace OfficeResourcesReservationSystem.Models
{
    public class Resource
    {
        public int ResourceId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public required string Name { get; set; }

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "IsAvailable is required")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "ResourceTypeId is required")]
        public int ResourceTypeId { get; set; }

    }
}
