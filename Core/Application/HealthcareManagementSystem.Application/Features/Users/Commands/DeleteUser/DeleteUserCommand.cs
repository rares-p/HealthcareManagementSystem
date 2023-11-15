using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid Id) : IRequest<DeleteUserCommandResponse>;
}
