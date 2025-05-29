using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayGifts.Services.DTOs.VotingSession
{
    public class CreateVotingSessionRequest
    {
        public int BirthdayPersonId { get; set; }
        public int CreatedById { get; set; }
    }
}
