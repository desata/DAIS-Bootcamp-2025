using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class DocumentInfo
    {
        public int DocumentId { get; set; }
        public string Title { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
    }

}
