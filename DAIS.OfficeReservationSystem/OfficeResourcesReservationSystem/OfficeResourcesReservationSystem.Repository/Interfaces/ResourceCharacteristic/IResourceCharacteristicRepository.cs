using OfficeResourcesReservationSystem.Repository.Base;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.ResourceCharacteristic
{
    public interface IResourceCharacteristicRepository : IBaseRepository<Models.ResourceCharacteristic, ResourceCharacteristicFilter, ResourceCharacteristicUpdate>
    {
    }
}
