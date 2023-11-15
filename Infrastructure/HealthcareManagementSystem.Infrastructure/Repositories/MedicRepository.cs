using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class MedicRepository : BaseRepository<Medic>, IMedicRepository
    {
        public MedicRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
