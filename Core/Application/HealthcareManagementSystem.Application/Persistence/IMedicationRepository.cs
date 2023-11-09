using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Application.Persistence
{
    public interface IMedicationRepository : IAsyncRepository<Medication>
    {
    }
}
