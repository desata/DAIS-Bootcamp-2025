using WikiSystem.Services.DTOs.Category;

namespace WikiSystem.Services.DTOs.Collection
{
    public class GetCollectionByCreatorResponse 
    {
        public List<CollectionInfo> Collections { get; set; }
        public int TotalCount { get; set; }
    }
}
