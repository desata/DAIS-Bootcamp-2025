using GiftChooserApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GiftChooserApp.Data
{
    public class GiftChooserAppDbContext : IdentityDbContext<Employee>
    {
        public GiftChooserAppDbContext(DbContextOptions<GiftChooserAppDbContext> options)
            : base(options)
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
                .HasForeignKey(vo => vo.VoteId);

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
                .OnDelete(DeleteBehavior.Restrict)
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

            // Seed employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Name = "Alice", BirthDate = new DateTime(1990, 5, 1) },
                new Employee { Name = "Bob", BirthDate = new DateTime(1988, 8, 15) },
                new Employee { Name = "Charlie",  BirthDate = new DateTime(1995, 12, 5) },
                new Employee { Name = "Daisy",  BirthDate = new DateTime(2002, 10, 2) },
                new Employee { Name = "Manuel",  BirthDate = new DateTime(1983, 1, 26) }
            );

            // Seed gifts
            modelBuilder.Entity<Gift>().HasData(
                new Gift { Id = 1, Description = "Coffee Mug" },
                new Gift { Id = 2, Description = "Gift Card" },
                new Gift { Id = 3, Description = "Bluetooth Speaker" },
                new Gift { Id = 4, Description = "Book" },
                new Gift { Id = 5, Description = "Bike" }
            );
        }
    }
}