using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Queries.GetById

{
    public class GetByIdMedicationQueryHandler : IRequestHandler<GetByIdMedicationQuery, MedicationDto>
    {
        private readonly IMedicationRepository _medicationRepository;

        public GetByIdMedicationQueryHandler(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<MedicationDto> Handle(GetByIdMedicationQuery request, CancellationToken cancellationToken)
        {
            var medication = await _medicationRepository.FindByIdAsync(request.Id);

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
