using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class MedicationRepository : BaseRepository<Medication>, IMedicationRepository
    {
        public MedicationRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }

        public async Task<Result<Medication>> FindByNameAsync(string name)
        {
            var medication = await Context.Set<Medication>().FirstOrDefaultAsync(medication => medication.Name == name);

            return medication == null ? Result<Medication>.Failure($"Medication with name {name} not found!") : Result<Medication>.Success(medication);
        }
    }
}
