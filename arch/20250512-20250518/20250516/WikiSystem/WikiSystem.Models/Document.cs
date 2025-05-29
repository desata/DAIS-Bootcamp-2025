using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class Document
    {
        [Required]
        public int DocumentId { get; set; }

        [Required]
        [StringLength(200)]
        public required string Title { get; set; }
        [Required]
        [StringLength(500)]
        public required string Tags { get; set; }
        [Required]
        public int AccessLevel { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public int CreatorId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
