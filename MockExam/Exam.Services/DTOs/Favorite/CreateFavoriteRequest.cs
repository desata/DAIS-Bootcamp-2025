using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Services.DTOs.Favorite
{
    public class CreateFavoriteRequest
    {
        public int UserId { get; set; }
        public int WorkplaceId { get; set; }
        public string Name { get; set; }
    }
}
