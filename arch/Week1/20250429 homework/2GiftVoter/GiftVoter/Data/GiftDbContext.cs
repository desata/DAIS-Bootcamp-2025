using GiftVoter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftVoter.Data
{
    public class GiftDbContext : IdentityDbContext<Employee>
    {
        public GiftDbContext(DbContextOptions<GiftDbContext> options) : base(options)
        {
        }

        
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteOption> VoteOptions { get; set; }
        public DbSet<VoteRecord> VoteRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint: One active vote per birthday person per year
            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.TargetEmployeeId, v.Year })
                .IsUnique();

            // Relationships
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.StartedBy)
                .WithMany(e => e.CreatedVotes)
                .HasForeignKey(v => v.StartedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.TargetEmployee)
                .WithMany()
                .HasForeignKey(v => v.TargetEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoteOption>()
                .HasOne(vo => vo.Vote)
                .WithMany(v => v.Options)
                .HasForeignKey(vo => vo.VoteId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<VoteOption>()
                .HasOne(vo => vo.Gift)
                .WithMany(g => g.VoteOptions)
                .HasForeignKey(vo => vo.GiftId); 

            modelBuilder.Entity<VoteRecord>()
                .HasOne(vr => vr.Vote)
                .WithMany(v => v.Records)
                .HasForeignKey(vr => vr.VoteId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<VoteRecord>()
                .HasOne(vr => vr.Voter)
                .WithMany(e => e.VoteRecords)
                .HasForeignKey(vr => vr.VoterEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoteRecord>()
                .HasOne(vr => vr.VoteOption)
                .WithMany(vo => vo.VoteRecords)
                .HasForeignKey(vr => vr.VoteOptionId)
                .OnDelete(DeleteBehavior.Restrict); 

            // One vote per person per vote
            modelBuilder.Entity<VoteRecord>()
                .HasIndex(vr => new { vr.VoteId, vr.VoterEmployeeId })
                .IsUnique();


            // === Seed Employees ===
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Name = "Alice Johnson", Username = "alice", Email = "alice@aaa.bb", PasswordHash = "hash1", BirthDate = new DateTime(1990, 5, 1) },
                new Employee { Name = "Bob Smith", Username = "bob", Email = "bob@aaa.bb", PasswordHash = "hash2", BirthDate = new DateTime(1988, 8, 15) },
                new Employee { Name = "Charlie Davis", Username = "charlie", Email = "charlie@aaa.bb", PasswordHash = "hash3", BirthDate = new DateTime(1995, 12, 5) }
            );

            // === Seed Gifts ===
            modelBuilder.Entity<Gift>().HasData(
                new Gift { Id = 1, Description = "Coffee Mug" },
                new Gift { Id = 2, Description = "Bluetooth Speaker" },
                new Gift { Id = 3, Description = "Amazon Gift Card" }
            );

            // === Seed a Vote (Bob's birthday, started by Alice) ===
            modelBuilder.Entity<Vote>().HasData(
                new Vote
                {
                    Id = 1,
                    StartedByEmployeeId = 1, // Alice
                    TargetEmployeeId = 2,    // Bob
                    Year = 2025,
                    IsActive = true,
                    StartTime = new DateTime(2025, 4, 29, 10, 0, 0, DateTimeKind.Utc),
                    EndTime = null
                }
            );

            // === Seed Vote Options for Vote 1 ===
            modelBuilder.Entity<VoteOption>().HasData(
                new VoteOption { Id = 1, VoteId = 1, GiftId = 1 }, // Coffee Mug
                new VoteOption { Id = 2, VoteId = 1, GiftId = 2 }, // Bluetooth Speaker
                new VoteOption { Id = 3, VoteId = 1, GiftId = 3 }  // Amazon Gift Card
            );
        }
    }
}
