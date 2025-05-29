using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiki.Models
{
    public class CollectionDocument
    {
        [Required]
        public int CollectionId { get; set; }
        [Required]
        public int DocumentId { get; set; }
    }
}
