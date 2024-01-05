using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Application.Features.Medics
{
    public class MedicDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public Department Department { get; set; } 
        public string AuthDataId { get; set; }
    }
}
