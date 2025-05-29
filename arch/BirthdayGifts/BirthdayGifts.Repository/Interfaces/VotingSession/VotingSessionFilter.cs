using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.VotingSession
{
    public class VotingSessionFilter
    {
        public SqlInt32? BirthdayPersonId { get; set; }
        public SqlInt32? CreatedById { get; set; }
        public SqlBoolean? IsActive { get; set; }
    }
}
