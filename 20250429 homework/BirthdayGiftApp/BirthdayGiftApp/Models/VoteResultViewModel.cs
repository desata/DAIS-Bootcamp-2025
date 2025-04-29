namespace BirthdayGiftApp.Models
{
    public class VoteResultViewModel
    {
        public int VoteId { get; set; }
        public Gift SelectedGift { get; set; }
        public List<GiftVoteSelection> VoteSelections { get; set; }
        public List<Gift> VoteOptions { get; set; }
    }

}
