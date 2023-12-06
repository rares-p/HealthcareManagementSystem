using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic
{
    public class CreateMedicDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
    }
}