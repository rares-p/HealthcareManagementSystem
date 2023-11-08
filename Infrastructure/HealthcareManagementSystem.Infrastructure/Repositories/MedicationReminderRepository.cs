using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class MedicationReminderRepository : BaseRepository<MedicationReminder>, IAsyncRepository<MedicationReminder>
    {
        public MedicationReminderRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
