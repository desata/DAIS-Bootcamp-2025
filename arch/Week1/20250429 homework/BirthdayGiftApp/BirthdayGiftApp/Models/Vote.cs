namespace BirthdayGiftApp.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public string TargetEmployeeId { get; set; }
        public Employee TargetEmployee { get; set; }

        public string StartedById { get; set; }
        public Employee StartedBy { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public ICollection<VoteOption> Options { get; set; }
    }

}
