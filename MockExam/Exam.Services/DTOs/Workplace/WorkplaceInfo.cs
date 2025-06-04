namespace Exam.Services.DTOs.Workplace
{
    public class WorkplaceInfo
    {
        public int WorkplaceId { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasDockingStation { get; set; }
        public bool HasWindow { get; set; }
        public bool HasPrinter { get; set; }
        public bool IsAvailable { get; set; }
        public string Location { get; set; }
    }
}
