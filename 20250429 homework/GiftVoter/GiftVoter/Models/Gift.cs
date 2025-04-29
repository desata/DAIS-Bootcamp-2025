namespace GiftVoter.Models
{
    public class Gift
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        public ICollection<VoteOption> VoteOptions { get; set; } = new List<VoteOption>();
    }
}