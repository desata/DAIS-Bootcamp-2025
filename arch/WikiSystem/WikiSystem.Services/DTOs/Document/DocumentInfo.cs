using WikiSystem.Models.Enums;

namespace WikiSystem.Services.DTOs.Document
{
    public class DocumentInfo
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public bool IsArchived { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
    }

}
