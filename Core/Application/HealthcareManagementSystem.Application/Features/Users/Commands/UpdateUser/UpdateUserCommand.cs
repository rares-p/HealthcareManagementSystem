using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
    {
        public Guid? Id { get; set; } = null;
        public string? FirstName { get; set; } = null;
        public string? LastName { get; set; } = null;
        public DateTime? DateOfBirth { get; set; } = null;
        public string? PhoneNumber { get; set; } = null;
        public string? Username { get; set; } = null;
        public string? Password { get; set; } = null;
        public string? Email { get; set; } = null;
    }
}
