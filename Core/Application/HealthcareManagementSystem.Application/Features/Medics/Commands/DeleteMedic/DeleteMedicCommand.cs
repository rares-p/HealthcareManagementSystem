using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.DeleteMedic
{
    public record DeleteMedicCommand(Guid Id) : IRequest<DeleteMedicCommandResponse>;

}
