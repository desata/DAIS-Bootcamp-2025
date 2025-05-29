namespace DAIS.WikiSystem.Services.DTOs.DocumentVersion
{
    public class CreateDocumentVersionResponse : DocumentVersionInfo
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
