using DAIS.WikiSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Document title is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Document title must be between 2 and 200 characters")]
        public required string Title { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Access level is required")]
        public AccessLevel AccessLevel { get; set; }
    }
}
