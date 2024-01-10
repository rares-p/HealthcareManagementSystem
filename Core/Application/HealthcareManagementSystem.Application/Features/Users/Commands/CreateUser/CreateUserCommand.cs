using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserCommandResponse>
    {
	    public string Username { get; set; } = string.Empty;
		public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }   
        public string AuthDataId { get; set; } 
    }
}
