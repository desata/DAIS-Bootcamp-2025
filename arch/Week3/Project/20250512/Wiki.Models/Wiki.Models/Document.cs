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
    public class Document
    {
        public int DocumentId { get; set; }

        [Required(ErrorMessage = "Document title is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Document title must be between 2 and 200 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Document tag is required")]
        [StringLength(500, ErrorMessage = "Document tags should be up to 500 characters")]
        public required string Tags { get; set; }

        [Required(ErrorMessage = "Access level is required")]
        public AccessLevel AccessLevel { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public int CreatorId { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }


    }
}
