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
            RuleFor(medic => medic.Email).Must(email => email is null || BeValidEmail(email)).WithMessage("Provided mail address is not valid");
        }
        private bool BeValidEmail(string? email)
        {
            return email == null || MailAddress.TryCreate(email, out _);
        }
        private bool ExistsDepartment(string department)
        {
            return Enum.IsDefined(typeof(Department), department);
        }
    }
}
