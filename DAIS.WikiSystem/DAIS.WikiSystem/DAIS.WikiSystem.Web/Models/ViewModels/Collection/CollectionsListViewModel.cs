using DAIS.WikiSystem.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace DAIS.WikiSystem.Web.Models.ViewModels.Collection
{
    public class CollectionsListViewModel
    {
        public List<CollectionViewModel> Collections { get; set; } = new();
        public CreateCollectionViewModel NewCollection { get; set; } = new();
    }

    public class CollectionViewModel
    {
        public int CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CreatorId { get; set; }
        public DateTime CreateDate { get; set; }
    }

    public class CreateCollectionViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        public int CreatorId { get; set; }
    }
}