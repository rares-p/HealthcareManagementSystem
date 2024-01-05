using FluentValidation;
using HealthcareManagementSystem.Domain.Data;
using System.Net.Mail;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.UpdateMedic
{
    public class UpdateMedicCommandValidator : AbstractValidator<UpdateMedicCommand>
    {
        public UpdateMedicCommandValidator()
        {
            RuleFor(medic => medic.FirstName).Must(firstName => firstName is null || !string.IsNullOrWhiteSpace(firstName)).WithMessage("Invalid first name");
            RuleFor(medic => medic.LastName).Must(lastName => lastName is null || !string.IsNullOrWhiteSpace(lastName)).WithMessage("Invalid last name");
            RuleFor(medic => medic.Department).Must(department => department is null || ExistsDepartment(department)).WithMessage("Provided department is not valid.");
        }
        private bool ExistsDepartment(Department? department)
        {
            return Enum.IsDefined(typeof(Department), department);
        }
    }
}
