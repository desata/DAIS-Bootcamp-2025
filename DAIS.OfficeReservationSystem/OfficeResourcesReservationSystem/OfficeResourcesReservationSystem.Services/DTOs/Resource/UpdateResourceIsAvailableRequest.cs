namespace OfficeResourcesReservationSystem.Services.DTOs.Resource
{
    public class UpdateResourceIsAvailableRequest
    {
        public int ResourceId { get; set; }

        public bool IsAvailable { get; set; }
    }
}
