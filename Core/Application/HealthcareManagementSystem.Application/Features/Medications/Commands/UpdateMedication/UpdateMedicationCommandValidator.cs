using FluentValidation;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.UpdateMedication
{
    public class UpdateMedicationCommandValidator : AbstractValidator<UpdateMedicationCommand>
    {
        public UpdateMedicationCommandValidator()
        {
            RuleFor(user => user.Id).NotNull().WithMessage("Id field is missing");
            RuleFor(medication => medication.Name).NotEmpty().WithMessage("Medication name is required");
        }
    }
}
