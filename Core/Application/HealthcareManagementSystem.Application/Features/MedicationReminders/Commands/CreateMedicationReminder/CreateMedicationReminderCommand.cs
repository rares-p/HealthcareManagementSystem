using MediatR;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder
{
    public class CreateMedicationReminderCommand : IRequest<CreateMedicationReminderCommandResponse>
    {
        public Guid UserId { get; set; }
        public Guid MedicationId { get; set; }
        public uint Dosage { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint DayInterval { get; set; } = 0;
        public List<float> HourList { get; set; } = new();
    }
}
