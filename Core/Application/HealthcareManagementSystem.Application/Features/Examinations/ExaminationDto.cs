using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Application.Features.Examinations
{
    public class ExaminationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid MedicId { get; set; }
        public DateTime Date { get; set; }
        public string Department { get; set; }
    }
}
