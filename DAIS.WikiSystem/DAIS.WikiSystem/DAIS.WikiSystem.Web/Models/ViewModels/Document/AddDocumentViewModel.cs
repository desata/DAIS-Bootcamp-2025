using DAIS.WikiSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Web.Models.ViewModels.Document
{
    public class AddDocumentViewModel
    {
        [Required(ErrorMessage = "Document title is required")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Document title must be between 2 and 200 characters")]
        public  string Title { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
        public string FilePath { get; set; }
        public string Version { get; set; }

    }
}