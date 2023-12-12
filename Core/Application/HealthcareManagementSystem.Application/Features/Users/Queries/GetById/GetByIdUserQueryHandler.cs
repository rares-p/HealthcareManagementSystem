using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserQuery, UserDto>
    {
        private readonly IUserRepository repository;

        public GetByIdUserHandler(IUserRepository repository)
        {
            this.repository = repository;
        }
        public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await repository.FindByIdAsync(request.Id);
            if (user.IsSuccess)
            {
                return new UserDto
                {
                    Id = user.Value.Id,
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
