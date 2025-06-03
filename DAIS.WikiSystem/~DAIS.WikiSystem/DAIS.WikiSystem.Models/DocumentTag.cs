using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Models
{
    public class DocumentTag
    {
        [Required]
        public int DocumentId { get; set; }

        [Required]
        public int TagId { get; set; }

    }
}