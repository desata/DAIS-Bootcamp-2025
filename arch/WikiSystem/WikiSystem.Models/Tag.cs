using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class Tag
    {
        public int TagId { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tags must be up to 100 characters")]
        public required string Name { get; set; }
    }
}
