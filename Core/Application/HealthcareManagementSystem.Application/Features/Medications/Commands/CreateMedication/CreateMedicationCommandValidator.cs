using FluentValidation;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication
{
    public class CreateMedicationCommandValidator : AbstractValidator<CreateMedicationCommand>
    {
        public CreateMedicationCommandValidator()
        {
            RuleFor(medication => medication.Name).NotEmpty().WithMessage("Medication name is required");
        }
    }
}
