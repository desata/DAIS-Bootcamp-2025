namespace OfficeResourcesReservationSystem.Services.DTOs.ResourceCharacteristic
{
    public class ResourceCharacteristicInfo
    {
        public int ResourceCharacteristicId { get; set; }
        public required string Name { get; set; }
        public required string Value { get; set; }
        public int ResourceId { get; set; }
        public string ResourceName { get; set; } 
    }
}
