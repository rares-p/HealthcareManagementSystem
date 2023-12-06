using FluentValidation;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.DeleteMedic
{
    public class DeleteMedicCommandValidator : AbstractValidator<DeleteMedicCommand>
    {
        public DeleteMedicCommandValidator()
        {
            RuleFor(medic => medic.Id).NotNull().WithMessage("Id cannot be null");
        }
    }
}
