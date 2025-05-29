using System.ComponentModel.DataAnnotations;

namespace Wiki.Models
{
    public class DocumentVersion
    {
        public int DocumentVersionsId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public required string Content { get; set; }
        

        [Required(ErrorMessage = "Version is required")]
        [StringLength(12, MinimumLength = 1, ErrorMessage = "Version must be up to 12 characters")]
        public required string Version { get; set; }

        public bool HasOtherVersions { get; set; }

        [Required(ErrorMessage = "Create date is required")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Document is required")]
        public int DocumentId { get; set; }
    }
}
