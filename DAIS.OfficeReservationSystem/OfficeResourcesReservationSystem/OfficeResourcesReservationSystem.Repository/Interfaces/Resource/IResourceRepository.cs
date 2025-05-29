using OfficeResourcesReservationSystem.Repository.Base;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Resource
{
    public interface IResourceRepository : IBaseRepository<Models.Resource, ResourceFilter, ResourceUpdate>
    {
    }
}
