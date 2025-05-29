namespace DAIS.WikiSystem.Services.DTOs.Collection
{
    public class AddDocumentsToCollectionResponse
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int DocumentsAddedCount { get; set; }
        public int CollectionId { get; set; }
    }
}
