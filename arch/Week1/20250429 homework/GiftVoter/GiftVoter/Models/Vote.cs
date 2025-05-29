namespace GiftVoter.Models
{
    public class Vote
    {
        public int Id { get; set; }

        public int StartedByEmployeeId { get; set; }
        public int BirthdayEmployeeId { get; set; }
        public int Year { get; set; }
        public bool IsActive { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        
        public Employee StartedBy { get; set; } = null!;
        public Employee BirthdayEmployee { get; set; } = null!;
        public ICollection<VoteOption> Options { get; set; } = new List<VoteOption>();
        public ICollection<VoteRecord> Records { get; set; } = new List<VoteRecord>();
    }
}