using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using MediatR;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic
{
    public class CreateMedicCommandHandler : IRequestHandler<CreateMedicCommand, CreateMedicCommandResponse>
    {
        private readonly IMedicRepository _repository;

        public CreateMedicCommandHandler(IMedicRepository repository)
        {
            this._repository = repository;
        }
        public async Task<CreateMedicCommandResponse> Handle(CreateMedicCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateMedicCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new CreateMedicCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
         
            var department = (Department)Enum.Parse(typeof(Department), request.Department);
            var medic = Medic.Create(request.FirstName, request.LastName, department, request.AuthDataId);

            if (!medic.IsSuccess)
            {
                return new CreateMedicCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { medic.Error }
                };
            }

            await _repository.AddAsync(medic.Value);

            return new CreateMedicCommandResponse
            {
                Success = true,
                Medic = new MedicDto
                {
                    Id = medic.Value.Id,
                    FirstName = medic.Value.FirstName,
                    LastName = medic.Value.LastName,
                    Department = medic.Value.Department.ToString(),
                    AuthDataId = medic.Value.AuthDataId
                }
            };
        }
    }
}
