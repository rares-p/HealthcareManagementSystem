using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Queries.GetById
{
    public class GetByIdMedicQueryHandler : IRequestHandler<GetByIdMedicQuery, MedicDto>
    {
        private readonly IMedicRepository repository;

        public GetByIdMedicQueryHandler(IMedicRepository repository)
        {
            this.repository = repository;
        }
        public async Task<MedicDto> Handle(GetByIdMedicQuery request, CancellationToken cancellationToken)
        {
            var Medic = await repository.FindByIdAsync(request.Id);
            if (Medic.IsSuccess)
            {
                return new MedicDto
                {
                    Id = Medic.Value.Id,
                    FirstName = Medic.Value.FirstName,
                    LastName = Medic.Value.LastName,
                    Department = Medic.Value.Department,
                    AuthDataId = Medic.Value.AuthDataId,
                };
            }
            return new MedicDto();
        }
    }
}
