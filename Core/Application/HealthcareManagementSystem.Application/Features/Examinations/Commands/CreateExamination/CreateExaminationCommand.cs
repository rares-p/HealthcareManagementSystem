using HealthcareManagementSystem.Domain.Data;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Examinations.Commands.CreateExamination
{
	public class CreateExaminationCommand : IRequest<CreateExaminationCommandResponse>
	{
		public Guid UserId { get; set; }
		public Guid MedicId { get; set; }
		public DateTime Date { get; set; }
		public string Department { get; set; }
	}
}
