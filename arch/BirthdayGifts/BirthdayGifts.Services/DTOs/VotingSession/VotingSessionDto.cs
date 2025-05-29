namespace BirthdayGifts.Services.DTOs.VotingSession
{
    public class VotingSessionDto
    {
        public int VotingSessionId { get; set; }
        public int BirthdayPersonId { get; set; }
        public string BirthdayPersonName { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public int BirthYear { get; set; }
    }

}
