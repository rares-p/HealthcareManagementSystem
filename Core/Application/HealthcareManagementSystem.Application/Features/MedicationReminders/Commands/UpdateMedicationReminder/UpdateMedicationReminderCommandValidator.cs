using FluentValidation;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.UpdateMedicationReminder
{
	public class UpdateMedicationReminderCommandValidator : AbstractValidator<UpdateMedicationReminderCommand>
	{
		public UpdateMedicationReminderCommandValidator()
		{
			RuleFor(medicationReminder => medicationReminder.DayInterval)
				.Must(dayInterval => dayInterval is null || dayInterval > 0u)
				.WithMessage("Interval has to be greater than 0!");
			RuleFor(medicationReminder => medicationReminder.Dosage)
				.Must(dosage => dosage is null || dosage > 0u)
				.WithMessage("Dosage has to greater than 0!");
			RuleFor(medicationReminder => medicationReminder.EndDate)
				.Must(endDate => endDate is null || endDate > DateTime.MinValue)
				.WithMessage("End date cannot be before current date!");
			RuleFor(medicationReminder => medicationReminder.StartDate)
				.Must(startDate => startDate is null || startDate > DateTime.MinValue)
				.WithMessage("Start date cannot be before current date!");
			RuleForEach(medication => medication.HourList)
				.ExclusiveBetween(0, 24).WithMessage("Hour has to be in 0 and 24 interval!");
		}
	}
}
