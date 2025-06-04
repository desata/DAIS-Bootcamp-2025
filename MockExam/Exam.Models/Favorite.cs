using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int WorkplaceId { get; set; }

        [Required(ErrorMessage = "Field is required")]
        public int UserId { get; set; }

    }
}
