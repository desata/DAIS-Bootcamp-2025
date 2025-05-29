using Microsoft.AspNetCore.Identity;

namespace GiftChooserApp.Models
{
    public class Employee : IdentityUser
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }

        // Navigation
        public ICollection<Vote> CreatedVotes { get; set; } = new List<Vote>();
        public ICollection<VoteRecord> VoteRecords { get; set; } = new List<VoteRecord>();
    }
}