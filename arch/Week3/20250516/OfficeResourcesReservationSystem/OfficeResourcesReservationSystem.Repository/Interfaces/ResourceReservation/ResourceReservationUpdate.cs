using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.ResourceReservation
{
    public class ResourceReservationUpdate
    {
        public SqlInt32? ReservationId { get; set; }
        public SqlInt32? ResourceId { get; set; }
    }
}
