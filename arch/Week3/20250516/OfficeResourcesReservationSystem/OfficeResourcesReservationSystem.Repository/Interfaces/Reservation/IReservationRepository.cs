using OfficeResourcesReservationSystem.Repository.Base;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Reservation
{
    public interface IReservationRepository : IBaseRepository<Models.Reservation, ReservationFilter, ReservationUpdate>
    {
    }
}
