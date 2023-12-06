using FluentValidation;
using System.Net.Mail;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Application.Features.Medics.Commands.CreateMedic
{
    public class CreateMedicCommandValidator : AbstractValidator<CreateMedicCommand>
    {
        public CreateMedicCommandValidator()
        {
            RuleFor(medic => medic.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(medic => medic.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(medic => medic.Department).Must(ExistsDepartment).WithMessage("Provided department is not valid.");
            RuleFor(medic => medic.Email).Must(BeValidEmail).WithMessage("Provided mail address is not valid.");
        }
        private bool BeValidEmail(string email)
        {
            return MailAddress.TryCreate(email, out _);
        }

        private bool ExistsDepartment(string department)
        {
            return Enum.IsDefined(typeof(Department), department);
        }
    }
}
