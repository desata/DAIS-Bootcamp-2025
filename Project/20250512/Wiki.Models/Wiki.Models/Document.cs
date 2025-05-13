using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{
    public class Document
    {
        public int DocumentId { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Document title must be between 2 and 200 characters")]

        public string Title { get; set; } = null!;

        [Required]
        public AccessLevel AccessLevel { get; set; }

        [Required]
        public string Version { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Document tags should be up to 500 characters")]
        public string? Tags { get; set; }

        [Required]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
