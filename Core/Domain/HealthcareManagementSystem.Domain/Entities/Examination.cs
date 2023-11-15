using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class Examination : AuditableEntity
    {
        private Examination(DateTime date, Department department, Guid userId, Guid medicId)
        {
            Id = Guid.NewGuid();
            Date = date;
            Department = department;
            UserId = userId;
            MedicId = medicId;
        }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid MedicId { get; private set; }
        public DateTime Date { get; private set; }
        public Department Department { get; private set; }

        public static Result<Examination> Create(DateTime date, Department department, Guid userId, Guid medicId)
        {
            return Result<Examination>.Success(new Examination(date, department, userId, medicId));
        }
    }
}
