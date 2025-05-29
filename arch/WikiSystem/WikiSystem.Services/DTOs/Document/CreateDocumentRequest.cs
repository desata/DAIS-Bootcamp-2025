namespace WikiSystem.Services.DTOs.Document
{
    public class CreateDocumentRequest
    {
        public string Title { get; set; }
        public int AccessLevel { get; set; }
        public bool IsArchived { get; set; }
        public int CreatorId { get; set; }
        public int CategoryId { get; set; }
    }
}
