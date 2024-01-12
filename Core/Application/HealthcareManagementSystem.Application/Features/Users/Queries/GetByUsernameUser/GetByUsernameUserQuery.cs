using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Queries.GetByUsernameUser
{
	public record GetByUsernameUserQuery(string Username) : IRequest<UserDto>;

}
