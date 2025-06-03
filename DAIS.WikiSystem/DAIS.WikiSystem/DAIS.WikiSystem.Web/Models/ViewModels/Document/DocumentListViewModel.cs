using DAIS.WikiSystem.Models.Enums;

namespace DAIS.WikiSystem.Web.Models.ViewModels.Document
{
    public class DocumentListViewModel
    {
        public List<DocumentViewModel> Documents { get; set; }
        public int TotalCount { get; set; }
    }


}
