using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{
    public class DocumentInformation
    {
        public int DocumentInformationId { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        [StringLength(12, MinimumLength = 1, ErrorMessage = "Version must be between 1 and 12 characters")]
        public required string Version { get; set; }

        [Required(ErrorMessage = "Content can't be empty")]
        public required string Content { get; set; }

        [Required]
        [ForeignKey("UploadedBy")]
        public int UploadedById { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; }
    }
}
