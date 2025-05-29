using OfficeResourcesReservationSystem.Repository.Base;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.ResourceReservation
{
    public interface IResourceReservationRepository : IBaseRepository<Models.ResourceReservation, ResourceReservationFilter, ResourceReservationUpdate>
    {
    }
}
