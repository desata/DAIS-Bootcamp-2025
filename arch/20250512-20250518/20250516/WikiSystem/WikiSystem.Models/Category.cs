using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class Category
    {

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
    }
}
