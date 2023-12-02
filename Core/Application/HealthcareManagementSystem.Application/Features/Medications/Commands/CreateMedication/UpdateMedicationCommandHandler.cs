using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.CreateMedication
{
    public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, CreateMedicationCommandResponse>
    {
        private readonly IMedicationRepository _repository;

        public CreateMedicationCommandHandler(IMedicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<CreateMedicationCommandResponse> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMedicationCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new CreateMedicationCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var medication = Medication.Create(request.Name);
            if (!medication.IsSuccess)
            {
                return new CreateMedicationCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = new List<string> { medication.Error }
                };
            }

            await _repository.AddAsync(medication.Value);

            return new CreateMedicationCommandResponse()
            {
                Success = true,
                Medication = new MedicationDto()
                {
                    Id = medication.Value.Id,
                    Name = medication.Value.Name
                }
            };
        }
    }
}
