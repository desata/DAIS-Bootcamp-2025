namespace GiftVoter.Models
{
    public class VoteOption
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public int GiftId { get; set; }



        public Vote Vote { get; set; } = null!;
        public Gift Gift { get; set; } = null!;
        public ICollection<VoteRecord> VoteRecords { get; set; } = new List<VoteRecord>();
    }
}