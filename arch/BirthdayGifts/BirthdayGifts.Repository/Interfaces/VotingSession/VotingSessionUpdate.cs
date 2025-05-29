using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.VotingSession
{
    public class VotingSessionUpdate
    {
        public SqlDateTime? StartDate { get; set; }
        public SqlDateTime? EndDate { get; set; }
        public SqlBoolean? IsActive { get; set; }
    }
}
