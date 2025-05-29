using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Models
{
    public class Collection
    {
        public int CollectionId { get; set; }

        [Required(ErrorMessage = "Collection name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Collection name must be between 3 and 50 characters")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Creator is required")]
        public int CreatorId { get; set; }
    }
}
