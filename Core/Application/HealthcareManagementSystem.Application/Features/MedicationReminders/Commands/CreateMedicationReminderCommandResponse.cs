﻿

using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands
{
    public class CreateMedicationReminderCommandResponse : BaseResponse
    {
        public CreateMedicationReminderDto? MedicationReminder { get; set; }
    }
}
