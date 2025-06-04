namespace Exam.Web.Models.ViewModels
{
    public class WorkplaceListViewModel
    {
        public int TotalCount { get; set; }
        public List<WorkplaceInfo> Workplaces { get; set; }

    }
    public class WorkplaceInfo
    {
        public int WorkplaceId { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasDockingStation { get; set; }
        public bool HasWindow { get; set; }
        public bool HasPrinter { get; set; }
        public bool IsAvailable { get; set; }
        public string Location { get; set; }

        public bool IsFavorite { get; set; }
        public int? FavoriteId { get; set; }
    }
}

