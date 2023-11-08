using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class MedicationRepository : BaseRepository<Medication>, IAsyncRepository<Medication>
    {
        public MedicationRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
