using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medications.Commands.DeleteMedication
{
    public class DeleteMedicationCommandHandler : IRequestHandler<DeleteMedicationCommand, DeleteMedicationCommandResponse>
    {
        private readonly IMedicationRepository _repository;

        public DeleteMedicationCommandHandler(IMedicationRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteMedicationCommandResponse> Handle(DeleteMedicationCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteMedicationCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new DeleteMedicationCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var result = await _repository.DeleteAsync(request.Id);

            if (!result.IsSuccess)
                return new DeleteMedicationCommandResponse()
                {
                    Success = false,
                    Message = result.Error
                };

            return new DeleteMedicationCommandResponse()
            {
                Success = true,
                Message = $"Medication with id {request.Id} deleted successfully"
            };
        }
    }
}
