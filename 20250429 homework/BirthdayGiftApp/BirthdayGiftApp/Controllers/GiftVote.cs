namespace BirthdayGiftApp.Controllers
{
    internal class GiftVote
    {
        public int BirthdayEmployeeId { get; set; }
        public string StartedByEmployeeId { get; set; }
        public object BirthdayDate { get; set; }
        public bool IsActive { get; set; }
    }
}