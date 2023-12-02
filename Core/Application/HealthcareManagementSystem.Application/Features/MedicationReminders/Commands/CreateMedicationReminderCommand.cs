using HealthcareManagementSystem.Domain.Entities;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands
{
    public class CreateMedicationReminderCommand : IRequest<CreateMedicationReminderCommandResponse>
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public Medication Medication { get; set; }
        public uint Dosage { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint DayInterval { get; set; } = 0;
        public List<float> HourList { get; set; } = new List<float>();
    }
}
