namespace BirthdayGiftApp.Models
{
    public class VoteRecord
    {
        public int Id { get; set; }

        public string VoterId { get; set; }
        public Employee Voter { get; set; }

        public int VoteOptionId { get; set; }
        public VoteOption VoteOption { get; set; }

        public DateTime VotedAt { get; set; }
    }
}