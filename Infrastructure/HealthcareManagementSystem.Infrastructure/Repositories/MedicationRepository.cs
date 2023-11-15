using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
