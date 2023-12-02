using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.UpdateMedication
{
    public class UpdateMedicationCommandResponse : BaseResponse
    {
        public MedicationDto Medication { get; set; }
    }
}
