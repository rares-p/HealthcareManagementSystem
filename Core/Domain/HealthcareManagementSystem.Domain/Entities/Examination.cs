using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class Examination : AuditableEntity
    {
        private Examination(DateTime date, Department department)
        {
            Id = Guid.NewGuid();
            Date = date;
            Department = department;
        }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid MedicId { get; private set; }
        public DateTime Date { get; private set; }
        public Department Department { get; private set; }

        public static Result<Examination> Create(DateTime date, Department department)
        {
            return Result<Examination>.Success(new Examination(date, department));
        }
    }
}
