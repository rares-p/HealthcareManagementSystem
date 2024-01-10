using FluentValidation;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Application.Features.Examinations.Commands.CreateExamination
{
	public class CreateExaminationCommandValidator : AbstractValidator<CreateExaminationCommand>
	{
		public CreateExaminationCommandValidator()
		{
			RuleFor(examination => examination.Date)
				.GreaterThan(DateTime.Now).WithMessage("Date cannot be before current date!");
			RuleFor(examination => examination.Department).Must(ExistsDepartment)
				.WithMessage("Provided department is not valid.");
		}

		private bool ExistsDepartment(string department)
		{
			return Enum.IsDefined(typeof(Department), department);
		}
	}
}
