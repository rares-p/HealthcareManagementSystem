using System.ComponentModel.DataAnnotations;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Application.Models.Identity.Registration
{
    public class MedicRegistrationModel : RegistrationModel
    {
        [Required(ErrorMessage = "Department is required")]
        public Department Department { get; set; }
    }
}
