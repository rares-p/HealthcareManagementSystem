using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class MedicationReminder : AuditableEntity
    {
        private MedicationReminder(uint dosage, DateTime startDate, DateTime endDate, uint dayInterval, List<float> hourList)
        {
            HourList = hourList;
            Dosage = dosage;
            StartDate = startDate;
            EndDate = endDate;
            DayInterval = dayInterval;
            HourList = hourList;
        }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid MedicationId { get; private set; }
        public uint Dosage { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public uint DayInterval { get; private set; }
        public List<float> HourList { get; private set; }
    }
}
