using System.Data.SqlTypes;

namespace OfficeResourcesReservationSystem.Repository.Interfaces.Reservation
{
    public class ReservationUpdate
    {

        public SqlDateTime? StartDate { get; set; }
        public SqlDateTime? EndDate { get; set; }
        public SqlString? Purpose { get; set; }
        public SqlInt32? NumberOfParticipants { get; set; }
        public SqlBoolean? IsActive { get; set; }

    }
}
