namespace WikiSystem.Services.DTOs.Tag
{
    public class GetAllTagsResponse
    {
        public List<TagInfo> Tags { get; set; };
        public int TotalCount { get; set; }
    }
}
