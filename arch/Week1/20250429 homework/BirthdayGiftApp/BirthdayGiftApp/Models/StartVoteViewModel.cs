namespace BirthdayGiftApp.Models
{
    public class StartVoteViewModel
    {
        public int BirthdayEmployeeId { get; set; }
        public List<int> SelectedGifts { get; set; } = new List<int>();
        public List<Employee> Employees { get; set; }
        public List<Gift> Gifts { get; set; }
    }

}
