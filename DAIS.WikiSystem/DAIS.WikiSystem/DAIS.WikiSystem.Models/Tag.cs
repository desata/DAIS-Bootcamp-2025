using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tag name must be between 2 and 50 characters")]
        public required string Name { get; set; }
    }
}
