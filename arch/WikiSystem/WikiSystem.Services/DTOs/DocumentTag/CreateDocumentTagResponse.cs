namespace WikiSystem.Services.DTOs.DocumentTag
{
    public class CreateDocumentTagResponse : DocumentTagInfo
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
