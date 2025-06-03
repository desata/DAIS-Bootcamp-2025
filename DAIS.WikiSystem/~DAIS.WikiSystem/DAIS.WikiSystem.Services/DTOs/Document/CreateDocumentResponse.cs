namespace DAIS.WikiSystem.Services.DTOs.Document
{
    public class CreateDocumentResponse : DocumentInfo
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}