namespace GiftChooserApp.Models
{
    public class Gift
    {
        public int Id { get; set; }

        public string Description { get; set; } = null!;

        // Navigation
        public ICollection<VoteOption> VoteOptions { get; set; } = new List<VoteOption>();
    }
}
