using MediatR;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.UpdateMedicationReminder
{
	public class UpdateMedicationReminderCommand : IRequest<UpdateMedicationReminderCommandResponse>
	{
		public Guid Id { get; set; }
		public Guid? MedicationId { get; set; }
		public uint? Dosage { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public uint? DayInterval { get; set; }
		public List<float>? HourList { get; set; } = new();
	}
}
