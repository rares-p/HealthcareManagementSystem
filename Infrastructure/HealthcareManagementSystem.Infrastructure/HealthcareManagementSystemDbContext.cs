using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Infrastructure
{
    public class HealthcareManagementSystemDbContext : DbContext
    {
        public HealthcareManagementSystemDbContext(DbContextOptions<HealthcareManagementSystemDbContext> options) : base(options)
        {
            
        }


    }
}
