using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Web.Models.ViewModels.Collection
{
    public class AddDocumentsToCollectionViewModel
    {
        public int CollectionId { get; set; }

        [Required(ErrorMessage = "Please select at least one document.")]
        public List<int> DocumentIds { get; set; }

        public List<SelectListItem> AvailableDocuments { get; set; }
    }
}