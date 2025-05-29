using System.ComponentModel.DataAnnotations;
using WikiSystem.Models.Enums;

namespace WikiSystem.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Document title is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Document title must be between 2 and 200 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Access level is required")]
        public AccessLevel AccessLevel { get; set; }

        [Required]
        public bool IsArchived { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }
    }
}
