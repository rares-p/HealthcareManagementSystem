namespace HealthcareManagementSystem.App.ViewModels
{
	public class MedicationReminderDto
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public Guid MedicationId { get; set; }
		public uint Dosage { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public uint DayInterval { get; set; } = 0;
		public List<float> HourList { get; set; } = new();
	}
}
