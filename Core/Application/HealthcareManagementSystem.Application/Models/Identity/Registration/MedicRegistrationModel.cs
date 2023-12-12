using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.Application.Models.Identity.Registration
{
    public class MedicRegistrationModel : RegistrationModel
    {
        [Required(ErrorMessage = "Department is required")]
        public string? Department { get; set; }
    }
}
