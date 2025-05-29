using BirthdayGiftApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<Employee>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Gift> Gifts { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<VoteOption> VoteOptions { get; set; }
    public DbSet<VoteRecord> VoteRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<VoteRecord>()
               .HasOne(vr => vr.VoteOption)
               .WithMany(vo => vo.VoteRecords)
               .HasForeignKey(vr => vr.VoteOptionId)
               .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Vote>()
       .HasOne(v => v.TargetEmployee)
       .WithMany()
       .HasForeignKey(v => v.TargetEmployeeId)
       .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Vote>()
            .HasOne(v => v.StartedBy)
            .WithMany()
            .HasForeignKey(v => v.StartedById)
            .OnDelete(DeleteBehavior.NoAction);

        // Also for VoteRecord → VoteOption if needed:
        builder.Entity<VoteRecord>()
            .HasOne(vr => vr.VoteOption)
            .WithMany(vo => vo.VoteRecords)
            .HasForeignKey(vr => vr.VoteOptionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
