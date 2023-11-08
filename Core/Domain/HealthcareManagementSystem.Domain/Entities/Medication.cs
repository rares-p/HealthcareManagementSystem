using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class Medication : AuditableEntity
    {
        private Medication(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Result<Medication> Create(string name)
        {
            if(string.IsNullOrWhiteSpace(name))
                return Result<Medication>.Failure("Medication name cannot be empty!");

            return Result<Medication>.Success(new Medication(name));
        }
    }
}
