using HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic;
using HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser;
using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Domain.Entities;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic
{
    public class UpdateMedicCommandHandler : IRequestHandler<UpdateMedicCommand, UpdateMedicCommandResponse>
    {
        private readonly IMedicRepository _repository;
        public UpdateMedicCommandHandler(IMedicRepository repository)
        {
            _repository = repository;
        }
        public async Task<UpdateMedicCommandResponse> Handle(UpdateMedicCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateMedicCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new UpdateMedicCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var medic = await _repository.FindByIdAsync((Guid)request.Id!);
            if (!medic.IsSuccess)
                return new UpdateMedicCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = new() { medic.Error }
                };

            if (request.FirstName != null)
            {
                var result = medic.Value.UpdateFirstName(request.FirstName);
                if (!result.IsSuccess)
                    return new UpdateMedicCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.LastName != null)
            {
                var result = medic.Value.UpdateLastName(request.LastName);
                if (!result.IsSuccess)
                    return new UpdateMedicCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.Department != null)
            {
                var department = request.Department ?? Department.EmergencyMedicine;
                medic.Value.UpdateDepartment(department);
            }

            try
            {
                await _repository.UpdateAsync(medic.Value);
            }
            catch (Exception)
            {
                return new UpdateMedicCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = new() { "An internal error occurred. Please try again later" }
                };
            }

            return new UpdateMedicCommandResponse()
            {
                Success = true,
                Medic = new MedicDto
                {
                    Id = medic.Value.Id,
                    FirstName = medic.Value.FirstName,
                    LastName = medic.Value.LastName,
                    Department = medic.Value.Department,
                    AuthDataId = medic.Value.AuthDataId
                }
            };
        }
    }
}
