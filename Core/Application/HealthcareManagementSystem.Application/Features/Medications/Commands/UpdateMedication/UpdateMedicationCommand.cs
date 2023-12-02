using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.UpdateMedication
{
    public class UpdateMedicationCommand : IRequest<UpdateMedicationCommandResponse>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
