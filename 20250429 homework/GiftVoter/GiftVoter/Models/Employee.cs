using Microsoft.AspNetCore.Identity;

namespace GiftVoter.Models
{
    public class Employee : IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime BirthDate { get; set; }


        public ICollection<Vote> CreatedVotes { get; set; } = new List<Vote>();
        public ICollection<VoteRecord> VoteRecords { get; set; } = new List<VoteRecord>();

    }
}
