using MediatR;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetByIdMedicationReminder
{
	public record GetByIdMedicationReminderQuery(Guid Id) : IRequest<GetByIdMedicationReminderResponse>;

}
