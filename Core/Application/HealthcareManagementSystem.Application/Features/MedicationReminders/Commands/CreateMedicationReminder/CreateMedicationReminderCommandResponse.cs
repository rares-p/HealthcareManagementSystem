using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder
{
    public class CreateMedicationReminderCommandResponse : BaseResponse
    {
        public CreateMedicationReminderDto? MedicationReminder { get; set; }
    }
}
