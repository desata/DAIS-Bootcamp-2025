using Exam.Models;

namespace Exam.Web.Models.ViewModels
{
    public class WorkplaceFavoriteViewModel
    {
        public int WorkplaceId { get; set; }
        public string Location { get; set; }
        public bool HasMonitor { get; set; }
        public bool HasDockingStation { get; set; }
        public bool HasWindow { get; set; }
        public bool HasPrinter { get; set; }

        public int? FavoriteId { get; set; }
        public bool IsFavorite => FavoriteId.HasValue;
    }
}