using Microsoft.AspNetCore.Identity;

namespace HealthcareManagementSystem.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
