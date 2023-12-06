using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Queries.GetById
{
    public record GetByIdMedicQuery(Guid Id) : IRequest<MedicDto>;
}
