using OfficeResourcesReservationSystem.Repository.Base;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.ResourceType
{
    public interface IResourceTypeRepository : IBaseRepository<Models.ResourceType, ResourceTypeFilter, ResourceTypeUpdate>
    {
    }
}
