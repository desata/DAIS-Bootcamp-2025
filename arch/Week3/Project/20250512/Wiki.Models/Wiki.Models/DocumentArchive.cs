using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiki.Models.Enums;

namespace Wiki.Models
{
    public class DocumentArchive
    {
        public int ArchiveId { get; set; }
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Tags { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required]
        public int CreatorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public required string Content { get; set; }

        [Required]
        public required string DocumentVersion { get; set; }

        [Required]
        public AccessLevel AccessLevel { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ArchivedAt { get; set; }

        [Required]
        public int OriginalDocumentId { get; set; }
    }
}
