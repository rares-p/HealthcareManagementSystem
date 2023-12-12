using HealthcareManagementSystem.Domain.Data;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic
{
    public class CreateMedicCommand : IRequest<CreateMedicCommandResponse>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string AuthDataId { get; set; }
    }
}
