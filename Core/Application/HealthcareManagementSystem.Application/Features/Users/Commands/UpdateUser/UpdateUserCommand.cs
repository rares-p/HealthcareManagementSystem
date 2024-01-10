using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
    {
        public Guid? Id { get; set; } = null;
        public string Username { get; set; }
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public DateTime? DateOfBirth { get; set; } = null;
    }
}
