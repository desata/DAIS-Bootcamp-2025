using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class CreateDocumentRequest
    {
        public string Title { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
        public List<int>? TagIds { get; set; }
    }
}