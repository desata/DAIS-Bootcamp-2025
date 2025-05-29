namespace BirthdayGiftApp.Models
{
    public class VoteViewModel
    {
        public int VoteId { get; set; }
        public int SelectedGiftId { get; set; }
        public List<Gift> GiftVoteOptions { get; set; }
    }

}
