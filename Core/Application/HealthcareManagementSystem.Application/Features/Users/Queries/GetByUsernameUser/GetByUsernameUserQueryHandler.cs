using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Queries.GetByUsernameUser
{
	public class GetByUsernameUserQueryHandler : IRequestHandler<GetByUsernameUserQuery, UserDto>
	{
		private readonly IUserRepository _repository;

		public GetByUsernameUserQueryHandler(IUserRepository repository)
		{
			_repository = repository;
		}

		public async Task<UserDto> Handle(GetByUsernameUserQuery request, CancellationToken cancellationToken)
		{
			var user = await _repository.GetByUsernameAsync(request.Username);
			if (user.IsSuccess)
			{
				return new UserDto
				{
					Id = user.Value.Id,
					Username = user.Value.Username,
					FirstName = user.Value.FirstName,
					LastName = user.Value.LastName,
					DateOfBirth = user.Value.DateOfBirth,
					AuthDataId = user.Value.AuthDataId
				};
			}
			return new UserDto();
		}
	}
}
