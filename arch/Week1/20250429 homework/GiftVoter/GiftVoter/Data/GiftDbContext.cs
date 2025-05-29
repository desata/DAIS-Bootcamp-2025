using GiftVoter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftVoter.Data
{
    public class GiftDbContext : IdentityDbContext<Employee>
    {
        public GiftDbContext(DbContextOptions<GiftDbContext> options)
            : base(options)
        {
        }
          
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Gift> Gifts { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteOption> VoteOptions { get; set; }
        public DbSet<VoteRecord> VoteRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique constraint: One active vote per birthday person per year
            modelBuilder.Entity<Vote>()
                .HasIndex(v => new { v.BirthdayEmployeeId, v.Year })
                .IsUnique();

            // Relationships
            modelBuilder.Entity<Vote>()
                .HasOne(v => v.StartedBy)
                .WithMany(e => e.CreatedVotes)
                .HasForeignKey(v => v.StartedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Vote>()
                .HasOne(v => v.BirthdayEmployee)
                .WithMany()
                .HasForeignKey(v => v.BirthdayEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoteOption>()
                .HasOne(vo => vo.Vote)
                .WithMany(v => v.Options)
                .HasForeignKey(vo => vo.VoteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VoteOption>()
                .HasOne(vo => vo.Gift)
                .WithMany(g => g.VoteOptions)
                .HasForeignKey(vo => vo.GiftId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .HasForeignKey(vr => vr.VoteOptionId);

            // One vote per person per vote
            modelBuilder.Entity<VoteRecord>()
                .HasIndex(vr => new { vr.VoteId, vr.VoterEmployeeId })
                .IsUnique();


            // === Seed Employees ===
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Alice Johnson", Username = "alice", Password = "hash1", BirthDate = new DateTime(1990, 5, 1) },
                new Employee { Id = 2, Name = "Bob Smith", Username = "bob", Password = "hash2", BirthDate = new DateTime(1997, 8, 15) },
                new Employee { Id = 3, Name = "Charlie Davis", Username = "charlie", Password = "hash3", BirthDate = new DateTime(1995, 12, 5) },
                new Employee { Id = 4, Name = "David Rob", Username = "david", Password = "hash4", BirthDate = new DateTime(2005, 2, 25) },
                new Employee { Id = 5, Name = "Tom Davis", Username = "tom", Password = "hash5", BirthDate = new DateTime(1998, 1, 2) }
            );

            // === Seed Gifts ===
            modelBuilder.Entity<Gift>().HasData(
                new Gift { Id = 1, Description = "Coffee Mug" },
                new Gift { Id = 2, Description = "Bluetooth Speaker" },
                new Gift { Id = 3, Description = "Amazon Gift Card" },
                new Gift { Id = 4, Description = "Bike" },
                new Gift { Id = 5, Description = "Car" },
                new Gift { Id = 6, Description = "Fridge" },
                new Gift { Id = 7, Description = "Spoon" }
            );

            // === Seed a Vote (Bob's birthday, started by Alice) ===
            modelBuilder.Entity<Vote>().HasData(
                new Vote
                {
                    Id = 1,
                    StartedByEmployeeId = 1, // Alice
                    BirthdayEmployeeId = 2,    // Bob
                    Year = 2025,
                    IsActive = true,
                    StartTime = new DateTime(2025, 4, 29, 10, 0, 0, DateTimeKind.Utc),
                    EndTime = null
                }
            );

            // === Seed Vote Options for Vote 1 ===
            modelBuilder.Entity<VoteOption>().HasData(
                new VoteOption { Id = 1, VoteId = 1, GiftId = 1 }, 
                new VoteOption { Id = 2, VoteId = 1, GiftId = 2 }, 
                new VoteOption { Id = 3, VoteId = 1, GiftId = 7 }  
            );
        }
    }
}
