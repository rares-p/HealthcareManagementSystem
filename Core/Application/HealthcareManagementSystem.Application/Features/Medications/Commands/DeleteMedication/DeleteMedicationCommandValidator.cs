using FluentValidation;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.DeleteMedication
{
    public class DeleteMedicationCommandValidator : AbstractValidator<DeleteMedicationCommand>
    {
        public DeleteMedicationCommandValidator()
        {
            RuleFor(medication => medication.Id).NotNull().WithMessage("Id cannot be null");
        }
    }
}
