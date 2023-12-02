using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.MedicationReminders.Commands
{
    public class CreateMedicationReminderHandler : IRequestHandler<CreateMedicationReminderCommand, CreateMedicationReminderCommandResponse>
    {
        private readonly IMedicationReminderRepository _medicationReminderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMedicationRepository _medicationRepository;

        public CreateMedicationReminderHandler(IMedicationReminderRepository medicationReminderRepository, IUserRepository userRepository, IMedicationRepository medicationRepository)
        {
            _medicationReminderRepository = medicationReminderRepository;
            _userRepository = userRepository;
            _medicationRepository = medicationRepository;
        }

        public async Task<CreateMedicationReminderCommandResponse> Handle(CreateMedicationReminderCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMedicationReminderCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new CreateMedicationReminderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var userExists = _userRepository.FindByIdAsync(request.User.Id).Result;
            if (!userExists.IsSuccess)
                return new CreateMedicationReminderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string>
                    {
                        userExists.Error
                    }
                };

            var medicationExists = _medicationRepository.FindByIdAsync(request.Medication.Id).Result;
            if (!medicationExists.IsSuccess)
                return new CreateMedicationReminderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string>
                    {
                        medicationExists.Error
                    }
                };

            var medicationReminder = MedicationReminder.Create(request.Dosage, request.StartDate, request.EndDate, request.DayInterval, request.HourList);
            if (!medicationReminder.IsSuccess)
                return new CreateMedicationReminderCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string>
                    {
                        medicationReminder.Error
                    }
                };

            await _medicationReminderRepository.AddAsync(medicationReminder.Value);
            return new CreateMedicationReminderCommandResponse()
            {
                Success = true,
                MedicationReminder = new CreateMedicationReminderDto()
                {
                    Id = medicationReminder.Value.Id,
                    Dosage = medicationReminder.Value.Dosage,
                    StartDate = medicationReminder.Value.StartDate,
                    EndDate = medicationReminder.Value.EndDate,
                    DayInterval = medicationReminder.Value.DayInterval,
                    HourList = new List<float>(medicationReminder.Value.HourList)
                }
            };

        }
    }
}
