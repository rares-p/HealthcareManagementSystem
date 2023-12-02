using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication
{
    public class CreateMedicationCommand : IRequest<CreateMedicationCommandResponse>
    {
        public string Name { get; set; }
    }
}
