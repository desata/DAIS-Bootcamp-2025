using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Workplace
    {
        public int WorkplaceId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool HasMonitor { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public bool HasDockingStation { get; set; }
        [Required(ErrorMessage = "Field is required")]
        public bool HasWindow { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool HasPrinter { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public bool IsAvailable { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Location must be between 3 and 100 characters")]
        public string Location { get; set; }

    }
}