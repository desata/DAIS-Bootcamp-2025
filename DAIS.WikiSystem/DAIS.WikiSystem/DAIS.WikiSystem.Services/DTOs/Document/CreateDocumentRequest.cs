using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class CreateDocumentRequest
    {
        public string Title { get; set; }
        public AccessLevel AccessLevel { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }        
        public List<int>? TagIds { get; set; }
        public string FilePath { get; set; }
        public string Version { get; set; }
        public string Content { get; set; }
    }
}
