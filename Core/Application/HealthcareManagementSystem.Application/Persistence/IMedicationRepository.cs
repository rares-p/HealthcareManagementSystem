using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Application.Persistence
{
    public interface IMedicationRepository : IAsyncRepository<Medication>
    {
        Task<Result<Medication>> FindByNameAsync(string name);
    }
}
