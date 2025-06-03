namespace DAIS.WikiSystem.Services.DTOs.Collection
{
    public class CreateCollectionResponse : CollectionInfo
    {
        public bool IsSuccess { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
