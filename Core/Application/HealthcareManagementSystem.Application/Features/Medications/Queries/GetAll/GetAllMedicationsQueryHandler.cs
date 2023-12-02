using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Queries.GetAll
{
    public class GetAllMedicationsQueryHandler : IRequestHandler<GetAllMedicationsQuery, GetAllMedicationsResponse>
    {
        private readonly IMedicationRepository _medicationRepository;

        public GetAllMedicationsQueryHandler(IMedicationRepository medicationRepository)
        {
            _medicationRepository = medicationRepository;
        }

        public async Task<GetAllMedicationsResponse> Handle(GetAllMedicationsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllMedicationsResponse();
            var result = await _medicationRepository.GetAllAsync();

            if (result.IsSuccess)
            {
                response.Medication = result.Value.Select(medication => new MedicationDto()
                {
                    Id = medication.Id,
                    Name = medication.Name,
                }).ToList();
            }

            return response;
        }
    }

}
