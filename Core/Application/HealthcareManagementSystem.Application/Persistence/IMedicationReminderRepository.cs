﻿using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Application.Persistence
{
    public interface IMedicationReminderRepository : IAsyncRepository<MedicationReminder>
    {
    }
}
