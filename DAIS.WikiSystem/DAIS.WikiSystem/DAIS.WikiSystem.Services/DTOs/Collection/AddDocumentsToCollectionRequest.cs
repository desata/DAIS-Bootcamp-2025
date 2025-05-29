namespace DAIS.WikiSystem.Services.DTOs.Collection
{
    public class AddDocumentsToCollectionRequest
    {
        public int CollectionId { get; set; }
        public List<int> DocumentIds { get; set; }
    }
}
