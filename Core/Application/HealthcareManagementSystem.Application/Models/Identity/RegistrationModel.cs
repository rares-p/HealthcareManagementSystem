using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.Application.Models.Identity
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Firstname is required")]
        public string? Firstname { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string? Lastname { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
