using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandResponse : BaseResponse
    {
        public CreateUserDto User { get; set; }
    }
}
