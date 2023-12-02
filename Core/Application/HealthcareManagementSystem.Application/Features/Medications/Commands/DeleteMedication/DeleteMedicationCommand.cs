using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.DeleteMedication
{
    public record DeleteMedicationCommand(Guid Id) : IRequest<DeleteMedicationCommandResponse>;
}
