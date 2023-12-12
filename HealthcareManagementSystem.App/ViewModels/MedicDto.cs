using HealthcareManagementSystem.Domain.Data;
using System.ComponentModel.DataAnnotations;

namespace HealthcareManagementSystem.App.ViewModels
{
    public class MedicDto
    {
		public Guid Id { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public Department Department { get; set; }
		public string Email { get; set; } = string.Empty;
	}
}