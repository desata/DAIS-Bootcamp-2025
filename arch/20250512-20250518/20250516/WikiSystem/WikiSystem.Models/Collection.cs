using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class Collection
    {

        [Required]
        public int CollectionId { get; set; }
        [Required]
        [StringLength(200)]
        public required string Name { get; set; }
        [Required]
        public int CreatorId { get; set; }
    }
}
