namespace OfficeResourcesReservationSystem.Services.DTOs.Resource
{
    public class ResourceInfo
    {
        public int ResourceId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public bool IsAvailable { get; set; }
        public int ResourceTypeId { get; set; }
        public string ResourceTypeName { get; set; } 
    }
}