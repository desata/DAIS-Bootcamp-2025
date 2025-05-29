namespace WikiSystem.Services.DTOs.DocumentVersion
{
    public class CreateDocumentVersionRequest
    {

        public string Version { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Content { get; set; }
        public int DocumentId { get; set; }
    }
}
