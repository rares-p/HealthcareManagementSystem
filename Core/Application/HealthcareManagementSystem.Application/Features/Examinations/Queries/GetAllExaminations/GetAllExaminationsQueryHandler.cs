using HealthcareManagementSystem.Application.Features.Medics.Queries.GetAll;
using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Examinations.Queries.GetAllExaminations
{
	public class GetAllExaminationsQueryHandler : IRequestHandler<GetAllExaminationsQuery, GetAllExaminationsResponse>
	{
		private readonly IExaminationRepository _repository;
		public GetAllExaminationsQueryHandler(IExaminationRepository repository)
		{
			_repository = repository;
		}

		public async Task<GetAllExaminationsResponse> Handle(GetAllExaminationsQuery request, CancellationToken cancellationToken)
		{
			var response = new GetAllExaminationsResponse();
			var result = await _repository.GetAllAsync();

			if (result.IsSuccess)
			{
				response.Examinations = result.Value.Select(examination => new ExaminationDto()
				{
					Id = examination.Id,
					UserId = examination.UserId,
					MedicId = examination.MedicId,
					Department = examination.Department.ToString(),
					Date = examination.Date
				}).ToList();
			}

			return response;
		}
	}
}
