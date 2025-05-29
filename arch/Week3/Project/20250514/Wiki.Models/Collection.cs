using System.ComponentModel.DataAnnotations;


namespace Wiki.Models
{
    public class Collection
    {
        public int CollectionId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2 , ErrorMessage = "Collection name must be between 2 and 200 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Creator of collection is required")]
        public int CreatorId { get; set; }

    }
}
