using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class MedicationReminderRepository : BaseRepository<MedicationReminder>, IMedicationReminderRepository
    {
        public MedicationReminderRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
