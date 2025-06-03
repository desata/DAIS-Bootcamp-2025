namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class UpdateDocumentVersionRequest
    {
        public int DocumentVersionId { get; set; }
        public bool IsArchivedNewStatus { get; set; }

    }
}
