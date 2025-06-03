namespace DAIS.WikiSystem.Services.DTOs.Collection
{
    public class CreateCollectionRequest
    {
        public required string Name { get; set; }
        public int CreatorId { get; set; }

    }
}
