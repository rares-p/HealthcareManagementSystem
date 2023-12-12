using HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser;
using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic
{
    public class CreateMedicCommandResponse : BaseResponse
    {
        public MedicDto Medic { get; set; }
    }
}