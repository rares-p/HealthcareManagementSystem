using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic
{
    public class UpdateMedicCommandResponse : BaseResponse
    {
        public MedicDto Medic { get; set; }
    }
}