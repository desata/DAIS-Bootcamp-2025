namespace WikiSystem.Services.DTOs.Collection
{
    public class CreateCollectionResponse : CollectionInfo
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
