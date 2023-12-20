using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.UpdateMedicationReminder
{
	public class UpdateMedicationReminderCommandResponse : BaseResponse
	{
		public MedicationRemindersDto MedicationReminder { get; set; }
	}
}
