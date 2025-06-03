namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class CreateDocumentVersionResponse : DocumentVersionInfo
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
