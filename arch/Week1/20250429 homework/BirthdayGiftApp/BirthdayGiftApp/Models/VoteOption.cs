namespace BirthdayGiftApp.Models
{
    public class VoteOption
    {
        public int Id { get; set; }

        public int VoteId { get; set; }
        public Vote Vote { get; set; }

        public int GiftId { get; set; }
        public Gift Gift { get; set; }

        public ICollection<VoteRecord> VoteRecords { get; set; }
    }
}