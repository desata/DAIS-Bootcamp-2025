namespace DAIS.WikiSystem.Services.DTOs.Collection
{
    public class GetAllCollectionByCreatorIdResponse
    {
        public List<CollectionInfo> Collections { get; set; }
        public int TotalCount { get; set; }
    }
}
