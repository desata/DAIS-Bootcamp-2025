using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Reservation
{
    public class ReservationUpdate
    {
        public SqlDateTime? EndDate { get; set; }
        public SqlBoolean? IsActive { get; set; }

    }
}
