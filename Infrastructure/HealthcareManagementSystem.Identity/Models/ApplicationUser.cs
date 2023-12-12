using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Firstname { get; set; }

        public string? Lastname { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
