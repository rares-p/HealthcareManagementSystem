using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class ExaminationRepository : BaseRepository<Examination>, IExaminationRepository
    {
        public ExaminationRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
