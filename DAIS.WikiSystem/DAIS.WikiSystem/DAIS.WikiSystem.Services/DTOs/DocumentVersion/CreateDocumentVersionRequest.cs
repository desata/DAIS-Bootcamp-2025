namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class CreateDocumentVersionRequest
    {
        public required string FilePath { get; set; }
        public int DocumentId { get; set; }
    }
}
