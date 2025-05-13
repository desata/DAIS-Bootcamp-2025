using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{
    public class DocumentArchive
    {
        public int ArchiveId { get; set; }

        [Required]
        public int DocumentId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [Required]
        public AccessLevel AccessLevel { get; set; }

        [Required]

        public string Version { get; set; } = null!;

        [StringLength(500)]
        public string? Tags { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime ArchivedAt { get; set; } 

        [Required]
        public string Content { get; set; } = null!;

        [Required]
        public int UploadedById { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; }
    }
}
