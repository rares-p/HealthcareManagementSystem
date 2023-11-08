namespace HealthcareManagementSystem.Infrastructure.Entities
{
    public class Medication
    {
        private Medication(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
