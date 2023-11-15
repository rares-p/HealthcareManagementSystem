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
            RuleFor(user => user.PhoneNumber).NotEmpty().WithMessage("Phone number is required")
                .Matches("0[0-9]{9}").WithMessage("Phone number is not valid");
            RuleFor(user => user.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(user => user.Email).Must(BeValidEmail).WithMessage("Provided mail address is not valid");
        }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            return (DateTime.Now - dateOfBirth).TotalDays / 365.2425 <= 120 &&
                   DateTime.Compare(DateTime.Now, dateOfBirth) >= 0;
        }

        private bool BeValidEmail(string email)
        {
            return MailAddress.TryCreate(email, out _);
        }
    }
}
