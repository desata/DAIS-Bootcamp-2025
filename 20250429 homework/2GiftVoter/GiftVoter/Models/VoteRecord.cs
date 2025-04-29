namespace GiftVoter.Models
{
    public class VoteRecord
    {
        public int Id { get; set; }

        public int VoteId { get; set; }
        public int VoterEmployeeId { get; set; }
        public int VoteOptionId { get; set; }
        public DateTime Timestamp { get; set; }

        // Navigation
        public Vote Vote { get; set; } = null!;
        public Employee Voter { get; set; } = null!;
        public VoteOption VoteOption { get; set; } = null!;
    }
}