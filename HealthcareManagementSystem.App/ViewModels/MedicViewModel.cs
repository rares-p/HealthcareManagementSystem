using HealthcareManagementSystem.Domain.Data;
using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.App.ViewModels
{
    public class MedicViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Medic's first name is required!")]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Department Department { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}