using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.UpdateMedication
{
    public class UpdateMedicationCommandHandler : IRequestHandler<UpdateMedicationCommand, UpdateMedicationCommandResponse>
    {
        private readonly IMedicationRepository _repository;

        public UpdateMedicationCommandHandler(IMedicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateMedicationCommandResponse> Handle(UpdateMedicationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMedicationCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new UpdateMedicationCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var medication = await _repository.FindByIdAsync(request.Id);
            if (!medication.IsSuccess)
                return new UpdateMedicationCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = new() { medication.Error }
                };

            var result = medication.Value.UpdateName(request.Name);

            if (!result.IsSuccess)
                return new UpdateMedicationCommandResponse()
                { Success = false, ValidationsErrors = new() { result.Error } };

            try
            {
                await _repository.UpdateAsync(medication.Value);
            }
            catch (Exception)
            {
                return new UpdateMedicationCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = new() { "An internal error occurred. Please try again later" }
                };
            }

            return new UpdateMedicationCommandResponse()
            {
                Success = true,
                Medication = new MedicationDto()
                {
                    Id = medication.Value.Id,
                    Name = medication.Value.Name,
                }
            };
        }
    }
}
