using HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser;
using HealthcareManagementSystem.Application.Features.Users.Commands.DeleteUser;
using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.DeleteMedic
{
    public class DeleteMedicCommandHandler : IRequestHandler<DeleteMedicCommand, DeleteMedicCommandResponse>
    {
        private readonly IMedicRepository _repository;

        public DeleteMedicCommandHandler(IMedicRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteMedicCommandResponse> Handle(DeleteMedicCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteMedicCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new DeleteMedicCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var result = await _repository.DeleteAsync(request.Id);

            if (!result.IsSuccess)
                return new DeleteMedicCommandResponse()
                {
                    Success = false,
                    Message = result.Error
                };

            return new DeleteMedicCommandResponse()
            {
                Success = true,
                Message = $"Medic with id {request.Id} deleted successfully"
            };
        }
    }
}
