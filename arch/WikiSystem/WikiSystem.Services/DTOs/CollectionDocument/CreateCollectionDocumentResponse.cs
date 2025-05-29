namespace WikiSystem.Services.DTOs.CollectionDocument
{
    public class CreateCollectionDocumentResponse : CollectionDocumentInfo
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
