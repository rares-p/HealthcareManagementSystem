using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetByIdMedicationReminder
{
	public class GetByIdMedicationReminderResponse : BaseResponse
	{
		public MedicationRemindersDto MedicationReminder { get; set; }
	}
}
