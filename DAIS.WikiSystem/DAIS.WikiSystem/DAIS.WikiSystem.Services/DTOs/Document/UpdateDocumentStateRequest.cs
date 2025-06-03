namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class UpdateDocumentStateRequest
    {
        public int DocumentId { get; set; }
        public bool IsDeletedNewStatus { get; set; }

    }
}
