using HealthcareManagementSystem.Application.Features.Medications.Queries.GetById;
using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Queries.GetByName

{
    public class GetByNameMedicationQueryHandler : IRequestHandler<GetByNameMedicationQuery, MedicationDto>
    {
        private readonly IMedicationRepository _medicationRepository;

        public GetByNameMedicationQueryHandler(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<MedicationDto> Handle(GetByNameMedicationQuery request, CancellationToken cancellationToken)
        {
            var medication = await _medicationRepository.FindByNameAsync(request.Name);

            if (medication.IsSuccess)
            {
                return new MedicationDto
                {
                    Id = medication.Value.Id,
                    Name = medication.Value.Name,
                };
            }

            return new MedicationDto();
        }
    }

}
