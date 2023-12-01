using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandResponse : BaseResponse
    {
        public UserDto User { get; set; }
    }
}
