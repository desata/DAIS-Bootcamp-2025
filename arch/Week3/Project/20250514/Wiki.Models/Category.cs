using System.ComponentModel.DataAnnotations;

namespace Wiki.Models
{
    public class Category
    {

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Catergory name must be between 2 and 100 characters")]
        public required string Name { get; set; }
    }
}
