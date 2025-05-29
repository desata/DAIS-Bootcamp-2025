using System.ComponentModel.DataAnnotations;

namespace OfficeResourcesReservationSystem.Models
{
    public class ResourceCharacteristic
    {

        public int ResourceCharacteristicId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]

        public required string Name { get; set; }

        [Required(ErrorMessage = "Value is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Value must be between 2 and 200 characters")]
        public required string Value { get; set; }

        [Required(ErrorMessage = "ResourceId is required")]
        public int ResourceId { get; set; }

    }
}
