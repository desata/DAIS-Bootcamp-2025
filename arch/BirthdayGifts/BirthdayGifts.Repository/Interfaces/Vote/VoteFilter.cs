using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.Vote
{
    public class VoteFilter
    {
        public SqlInt32? VoterId { get; set; }
        public SqlInt32? VotingSessionId { get; set; }
    }
}