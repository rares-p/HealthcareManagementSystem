using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Queries.GetById
{
    public record GetByIdMedicationQuery(Guid Id) : IRequest<MedicationDto>;
}
