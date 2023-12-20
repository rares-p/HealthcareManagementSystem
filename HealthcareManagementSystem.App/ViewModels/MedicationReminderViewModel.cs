namespace HealthcareManagementSystem.App.ViewModels
{
	public class MedicationReminderViewModel
	{
		public Guid UserId { get; set; } = Guid.Empty;
		public Guid MedicationId { get; set; } = Guid.Empty;
		public uint Dosage { get; set; } = 0;
		public DateTime StartDate { get; set; } = DateTime.MinValue;
		public DateTime EndDate { get; set; } = DateTime.MinValue;
		public uint DayInterval { get; set; } = 0;
		public List<float> HourList { get; set; } = new();
	}
}
