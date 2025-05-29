using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class Collection
    {
        public int CollectionId { get; set; }

        [Required(ErrorMessage = "Collection name is required")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 200 characters")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Creator is required")]
        public int CreatorId { get; set; }
    }
}
