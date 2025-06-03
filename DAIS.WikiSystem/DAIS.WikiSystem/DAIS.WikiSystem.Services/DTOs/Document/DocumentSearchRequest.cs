using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class DocumentSearchRequest
    {
        public bool? IsDeleted { get; set; }
        public string? Title { get; set; }
        public int? CreatorId { get; set; }
        public int? CategoryId { get; set; }
        public int? DocumentId { get; set; }
        public AccessLevel? MaxAccessLevel { get; set; }
    }
}
