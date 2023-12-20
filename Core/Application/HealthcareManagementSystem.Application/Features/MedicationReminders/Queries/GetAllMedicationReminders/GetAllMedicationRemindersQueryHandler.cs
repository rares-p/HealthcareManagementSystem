using HealthcareManagementSystem.Application.Features.Medics.Queries.GetAll;
using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Queries.GetAllMedicationReminders
{
    public class GetAllMedicationRemindersQueryHandler : IRequestHandler<GetAllMedicationRemindersQuery, GetAllMedicationRemindersResponse>
    {
        private readonly IMedicationReminderRepository _repository;

        public GetAllMedicationRemindersQueryHandler(IMedicationReminderRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetAllMedicationRemindersResponse> Handle(GetAllMedicationRemindersQuery request, CancellationToken cancellationToken)
        {
            var response = new GetAllMedicationRemindersResponse();
            var result = await _repository.GetAllAsync();

            if (result.IsSuccess)
            {
                response.MedicationReminders = result.Value.Select(medicationReminder => new MedicationRemindersDto()
                {
                    Id = medicationReminder.Id,
                    UserId = medicationReminder.UserId,
                    MedicationId = medicationReminder.MedicationId,
                    Dosage = medicationReminder.Dosage,
                    StartDate = medicationReminder.StartDate,
                    EndDate = medicationReminder.EndDate,
                    DayInterval = medicationReminder.DayInterval,
                    HourList = medicationReminder.HourList
                }).ToList();
            }

            return response;
        }
    }
}
