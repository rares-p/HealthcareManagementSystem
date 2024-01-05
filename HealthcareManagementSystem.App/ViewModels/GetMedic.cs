using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.App.ViewModels
{
    public class GetMedic
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Department Department { get; set; }
        public string AuthDataId { get; set; }
    }
}
