namespace Exam.Services.DTOs.Workplace
{
    public class UpdateWorkplaceAvailabilityRequest
    {
        public int WorkplaceId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
