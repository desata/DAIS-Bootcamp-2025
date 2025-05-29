using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.WebApp.Models.ViewModels
{
    public class DocumentListViewModel
    {
        public List<DocumentViewModel> Documents { get; set; }
        public int TotalCount { get; set; }
    }
    public class DocumentViewModel
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string CategoryName { get; set; }
        public List<string> Tags { get; set; }
    }
}