using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftVotingSystem.Models.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int EmployeeId { get; set; }
        public int GiftId { get; set; }
        public int PoolId { get; set; }
    }
}
