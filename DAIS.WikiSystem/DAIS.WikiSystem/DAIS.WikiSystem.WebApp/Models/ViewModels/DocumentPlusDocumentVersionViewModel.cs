using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.WebApp.Models.ViewModels
{
    public class DocumentPlusDocumentVersionViewModel
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string CategoryName { get; set; }
        public List<string> Tags { get; set; }
        public string Version { get; set; }
        public string Content { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
