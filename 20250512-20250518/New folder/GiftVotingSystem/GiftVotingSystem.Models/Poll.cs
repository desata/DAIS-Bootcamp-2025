using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftVotingSystem.Models
{
    public class Poll
    {
        public int PollId { get; set; }
        public int CreatorId { get; set; }
        public int BirthdayPersonId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1900, 2999, ErrorMessage = "Year must be between 1990 and 2999")]
        public int YearToCelebrate { get; set; }

    }
}
