using System.Threading;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic;
using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Application.Features.Examinations.Commands.CreateExamination
{
	public class CreateExaminationCommandHandler : IRequestHandler<CreateExaminationCommand, CreateExaminationCommandResponse>
	{
		private readonly IExaminationRepository _repository;

		public CreateExaminationCommandHandler(IExaminationRepository repository)
		{
			this._repository = repository;
		}

		public async Task<CreateExaminationCommandResponse> Handle(CreateExaminationCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateExaminationCommandValidator();
			var validatorResult = await validator.ValidateAsync(request, cancellationToken);

			if (!validatorResult.IsValid)
				return new CreateExaminationCommandResponse()
				{
					Success = false,
					ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
				};

			var department = (Department)Enum.Parse(typeof(Department), request.Department);
			var examination = Examination.Create(request.Date, department, request.UserId, request.MedicId);

			if (!examination.IsSuccess)
			{
				return new CreateExaminationCommandResponse
				{
					Success = false,
					ValidationsErrors = new List<string> { examination.Error }
				};
			}

			await _repository.AddAsync(examination.Value);

			return new CreateExaminationCommandResponse
			{
				Success = true,
				Examination = new ExaminationDto()
				{
					Id = examination.Value.Id,
					UserId = examination.Value.UserId,
					MedicId = examination.Value.MedicId,
					Department = examination.Value.Department.ToString(),
					Date = examination.Value.Date
				}
			};
		}
	}
}
