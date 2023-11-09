using HealthcareManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Infrastructure
{
    public class HealthcareManagementSystemDbContext : DbContext
    {
        public HealthcareManagementSystemDbContext(DbContextOptions<HealthcareManagementSystemDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationReminder> MedicationReminders { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLogin>().HasNoKey();
            modelBuilder.HasDefaultSchema("healthcaremanagementsystem");
        }
    }
}
