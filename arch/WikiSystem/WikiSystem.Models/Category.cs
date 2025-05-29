using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be up to 100 characters")]
        public required string Name { get; set; }
    }
}
