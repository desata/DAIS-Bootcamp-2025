using System.ComponentModel.DataAnnotations;

namespace Wiki.Models
{
    public class CollectionDocument
    {
        [Required]
        public int CollectionId { get; set; }
        [Required]
        public int DocumentId { get; set; }
    }
}
