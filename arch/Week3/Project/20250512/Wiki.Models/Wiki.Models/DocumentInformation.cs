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
    public class DocumentInformation
    {
        public int DocumentInformationId { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public required string Content { get; set; }
        

        [Required(ErrorMessage = "Version is required")]
        [StringLength(12, MinimumLength = 1, ErrorMessage = "Version must be up to 12 characters")]
        public required string DocumentVersion { get; set; }

        public bool HasOldVersions { get; set; }

        [Required(ErrorMessage = "Create date is required")]
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Document is required")]
        public int DocumentId { get; set; }
    }
}
