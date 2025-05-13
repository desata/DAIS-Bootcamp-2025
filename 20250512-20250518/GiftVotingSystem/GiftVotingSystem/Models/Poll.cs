using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GiftVotingSystem.Models.Models
{
    public class Poll
    {
        public int PollId { get; set; }
        public int CreatorId { get; set; }

        public int BirthdayPersonId { get; set; }
 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public int YearToCelebrate { get; set; }


    }
}
