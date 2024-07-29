using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Gym_Mgt_System.Models;

namespace Gym_Mgt_System.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<MembershipPlan> MembershipPlans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure MembershipPlan relationships
            modelBuilder.Entity<Member>()
                .HasOne<MembershipPlan>()
                .WithMany()
                .HasForeignKey(m => m.Id);

            // Configure Trainer relationships
            modelBuilder.Entity<Member>()
                .HasOne<Trainer>()
                .WithMany(t => t.Members)
                .HasForeignKey(m => m.Id);

            // Additional configurations
            modelBuilder.Entity<MembershipPlan>()
                .Property(m => m.Cost)
                .HasColumnType("decimal(18,2)");
        }
    }
}
