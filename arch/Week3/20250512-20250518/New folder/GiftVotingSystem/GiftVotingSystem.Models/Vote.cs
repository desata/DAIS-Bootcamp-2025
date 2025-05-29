using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftVotingSystem.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int GiftId { get; set; }
        [Required]
        public int PoolId { get; set; }
    }
}
