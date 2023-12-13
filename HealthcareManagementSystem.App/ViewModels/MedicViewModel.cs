using HealthcareManagementSystem.Domain.Data;
using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.App.ViewModels
{
    public class MedicViewModel
    {
        public string Username { get; set; }
        [Required(ErrorMessage = "Medic's first name is required!")]
        public string FirstName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Medic's last name is required!")]
		public string LastName { get; set; } = string.Empty;
		[Required(ErrorMessage = "Medic must be part of a department!")]
		public string Department { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }
    }
}