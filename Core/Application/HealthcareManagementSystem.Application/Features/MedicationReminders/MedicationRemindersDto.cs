﻿

using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders
{
    public class MedicationRemindersDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MedicationId { get; set; }
        public uint Dosage { get; set; } = 0;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public uint DayInterval { get; set; } = 0;
        public List<float> HourList { get; set; } = new();
    }
}
