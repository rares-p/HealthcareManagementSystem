using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Queries.GetById
{
    public record GetByIdUserQuery(Guid Id) : IRequest<UserDto>;
}
