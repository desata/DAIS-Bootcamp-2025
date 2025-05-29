using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class CollectionDocument
    {
        [Required]
        public int CollectionId { get; set; }
        [Required]
        public int DocumentId { get; set; }
    }
}
