using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gym_Mgt_System.Models;

namespace Gym_Mgt_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        internal readonly object TrainerViewModel;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<MembershipPlan> MembershipPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Member to MembershipPlan relationship
            modelBuilder.Entity<Member>()
                .HasOne(m => m.MembershipPlan)
                .WithMany(mp => mp.Members)
                .HasForeignKey(m => m.MembershipPlanId);

            // Configure Member to Trainer relationship
            modelBuilder.Entity<Member>()
                .HasOne(m => m.Trainer)
                .WithMany(t => t.Members)
                .HasForeignKey(m => m.TrainerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Additional configurations
            modelBuilder.Entity<MembershipPlan>()
                .Property(m => m.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
