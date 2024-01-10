using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.App.ViewModels
{
	public class CreateExaminationViewModel
	{
        public Guid UserId { get; set; }
        public Guid MedicId { get; set; }
        public DateTime Date { get; set; }
        public string Department { get; set; }
    }
}
