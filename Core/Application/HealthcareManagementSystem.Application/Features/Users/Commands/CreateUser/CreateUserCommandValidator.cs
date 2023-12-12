using FluentValidation;
using System.Net.Mail;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(user => user.DateOfBirth).Must(BeValidDateOfBirth).WithMessage("Date of birth is invalid");
        }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            return (DateTime.Now - dateOfBirth).TotalDays / 365.2425 <= 120 &&
                   DateTime.Compare(DateTime.Now, dateOfBirth) >= 0;
        }
    }
}
