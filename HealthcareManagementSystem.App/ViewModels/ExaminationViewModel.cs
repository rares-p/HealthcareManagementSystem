using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.App.ViewModels
{
	public class ExaminationViewModel
	{
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid MedicId { get; private set; }
        public DateTime Date { get; private set; }
        public Department Department { get; private set; }
    }
}
