namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class CreateDocumentVersionRequest
    {
        public string Content { get; set; }
        public string Version { get; set; }
        public bool IsArchived { get; set; }
        public int DocumentId { get; set; }

    }
}
