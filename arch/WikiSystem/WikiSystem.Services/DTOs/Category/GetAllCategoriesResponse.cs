namespace WikiSystem.Services.DTOs.Category
{
    public class GetAllCategoriesResponse
    {
        public List<CategoryInfo> Categories { get; set; }
        public int TotalCount { get; set; }
    }
}
