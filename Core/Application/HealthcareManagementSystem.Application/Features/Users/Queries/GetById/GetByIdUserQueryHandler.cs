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
                    Username = user.Value.Username,
                    FirstName = user.Value.FirstName,
                    LastName = user.Value.LastName,
                    Email = user.Value.Email,
                    DateOfBirth = user.Value.DateOfBirth,
                    PhoneNumber = user.Value.PhoneNumber
                };
            }
            return new UserDto();
        }
    }
}
