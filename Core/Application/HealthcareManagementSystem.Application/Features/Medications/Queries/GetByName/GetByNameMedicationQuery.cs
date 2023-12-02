using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Queries.GetByName
{
    public record GetByNameMedicationQuery(string Name) : IRequest<MedicationDto>;
}
