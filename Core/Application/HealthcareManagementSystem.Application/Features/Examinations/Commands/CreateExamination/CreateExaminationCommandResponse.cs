using HealthcareManagementSystem.Application.Features.MedicationReminders.Commands.CreateMedicationReminder;
using HealthcareManagementSystem.Application.Responses;

namespace HealthcareManagementSystem.Application.Features.Examinations.Commands.CreateExamination
{
    public class CreateExaminationCommandResponse : BaseResponse
	{
		public ExaminationDto? Examination { get; set; }

	}
}
