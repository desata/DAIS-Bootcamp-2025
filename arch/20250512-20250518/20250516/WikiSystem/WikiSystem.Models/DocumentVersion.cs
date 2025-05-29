using System.ComponentModel.DataAnnotations;

namespace WikiSystem.Models
{
    public class DocumentVersion
    {

        [Required]
        public int DocumentVersionsId { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        [StringLength(12)]
        public required string Version { get; set; }
        [Required]
        public bool HasOtherVersions { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Required]
        public int DocumentId { get; set; }

    }
}
