using HealthcareManagementSystem.Application.Contracts.Interfaces;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Infrastructure
{
    public class HealthcareManagementSystemDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public HealthcareManagementSystemDbContext(DbContextOptions<HealthcareManagementSystemDbContext> options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Medic> Medics { get; set; }
        public DbSet<Examination> Examinations { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<MedicationReminder> MedicationReminders { get; set;}

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = _currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.CreatedDate = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Entity.LastModifiedBy = _currentUserService.GetCurrentClaimsPrincipal()?.Claims.FirstOrDefault(c => c.Type == "name")?.Value!;
                    entry.Entity.LastModifiedDate = DateTime.UtcNow;
                }
            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("healthcaremanagementsystem");
        }
    }
}
