using FluentValidation;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder
{
    public class CreateMedicationReminderCommandValidator : AbstractValidator<CreateMedicationReminderCommand>
    {
        public CreateMedicationReminderCommandValidator()
        {
            RuleFor(medicationReminder => medicationReminder.DayInterval)
                .NotEmpty().WithMessage("Interval must not be empty!")
                .GreaterThan(0u).WithMessage("Interval has to be greater than 0!");
            RuleFor(medicationReminder => medicationReminder.Dosage)
                .GreaterThan(0u).WithMessage("Dosage has to greater than 0!");
            RuleFor(medicationReminder => medicationReminder.EndDate)
                .GreaterThan(DateTime.Now).WithMessage("End date cannot be before current date!");
            RuleForEach(medication => medication.HourList)
                .ExclusiveBetween(0, 24).WithMessage("Hour has to be in 0 and 24 interval!");
            RuleFor(medication => medication.StartDate)
                .GreaterThan(DateTime.Now).WithMessage("Start date cannot be before current date!")
                .Must((model, startDate) => startDate < model.EndDate).WithMessage("Start date must be before end date!");
        }
    }
}
