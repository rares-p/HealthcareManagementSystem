using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Queries.GetAll
{
    public class GetAllMedicsQueryHandler : IRequestHandler<GetAllMedicsQuery, GetAllMedicsResponse>
    {
        private readonly IMedicRepository _repository;

        public GetAllMedicsQueryHandler(IMedicRepository repository)
        {
            _repository = repository;
        }
        public async Task<GetAllMedicsResponse> Handle(GetAllMedicsQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllMedicsResponse();
            var result = await _repository.GetAllAsync();

            if (result.IsSuccess)
            {
                response.Medics = result.Value.Select(medic => new MedicDto()
                {
                    Id = medic.Id,
                    FirstName = medic.FirstName,
                    LastName = medic.LastName,
                    Department = medic.Department.ToString(),
                    Email = medic.Email,
                }).ToList();
            }

            return response;
        }
    }
}
