using System.Data.SqlTypes;

namespace BirthdayGifts.Repository.Interfaces.Vote
{
    public class VoteUpdate
    {
        public SqlInt32? GiftId { get; set; }
        public SqlDateTime? VoteDate { get; set; }
    }
}