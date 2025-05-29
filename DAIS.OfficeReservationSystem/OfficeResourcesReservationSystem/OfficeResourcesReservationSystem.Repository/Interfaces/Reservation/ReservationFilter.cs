using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Reservation
{
    public class ReservationFilter
    {
        public SqlString? StartDate { get; set; }
        public SqlInt32? CreatorId { get; set; }
        public SqlBoolean? IsActive { get; set; }
    }
}
