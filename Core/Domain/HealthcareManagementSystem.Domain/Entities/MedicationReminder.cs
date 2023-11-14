using System.ComponentModel.DataAnnotations.Schema;
using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class MedicationReminder : AuditableEntity
    {
        private MedicationReminder(uint dosage, DateTime startDate, DateTime endDate, uint dayInterval, List<float> hourList)
        {
            Id = Guid.NewGuid();
            HourList = hourList;
            Dosage = dosage;
            StartDate = startDate;
            EndDate = endDate;
            DayInterval = dayInterval;
            HourList = hourList;
        }

        public Guid Id { get; private set; }
        public User User { get; private set; }
        public Medication Medication { get; private set; }
        public uint Dosage { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public uint DayInterval { get; private set; }
        public List<float> HourList { get; private set; }

        public Result<MedicationReminder> Create(uint dosage, DateTime startDate, DateTime endDate, uint dayInterval, List<float> hourList)
        {
            if(dosage is < 1 or > 10)
                return Result<MedicationReminder>.Failure("Medication dosage is not valid!");

            if(DateTime.Compare(startDate, endDate) >= 0)
                return Result<MedicationReminder>.Failure("Start Date must be before End Date!");

            if(dayInterval < 1)
                return Result<MedicationReminder>.Failure("Day Interval must be higher or equal to 1!");

            if(hourList.Count < 1)
                return Result<MedicationReminder>.Failure("Hour List is empty!");

            foreach (var hour in hourList)
            {
                if(hour < 0 || hour > 23.59)
                    return Result<MedicationReminder>.Failure("All hours must be between 0:00 and 23:59!");

                if(hour - Math.Truncate(hour) > 0.59)
                    return Result<MedicationReminder>.Failure("Minutes is an hour must be between 0 and 59!");
            }

            return Result<MedicationReminder>.Success(new MedicationReminder(dosage, startDate, endDate, dayInterval, hourList));
        }
    }
}
