using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication
{
    public class CreateMedicationCommandResponse : BaseResponse
    {
        public MedicationDto Medication { get; set; }
    }
}
