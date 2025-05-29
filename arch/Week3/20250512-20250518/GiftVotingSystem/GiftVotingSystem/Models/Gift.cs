using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftVotingSystem.Models.Models
{
    public class Gift
    {
        public int GiftId { get; set; }
        public string Name { get; set; } 
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
