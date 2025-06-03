namespace DAIS.WikiSystem.Services.DTOs.Category
{
    public class GetAllCategoriesResponse
    {
        public List<CategoryInfo> Categories { get; set; }
        public int Count { get; set; }
    }
}