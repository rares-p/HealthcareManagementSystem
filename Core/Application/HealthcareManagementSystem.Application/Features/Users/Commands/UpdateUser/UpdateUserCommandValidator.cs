using FluentValidation;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(user => user.Id).NotNull().WithMessage("Id field is missing");
            RuleFor(user => user.FirstName).Must(firstName => firstName is null || !string.IsNullOrWhiteSpace(firstName)).WithMessage("Invalid first name");
            RuleFor(user => user.LastName).Must(lastName => lastName is null || !string.IsNullOrWhiteSpace(lastName)).WithMessage("Invalid last name");
            RuleFor(user => user.DateOfBirth).Must(BeValidDateOfBirth).WithMessage("Date of birth is invalid");
        }

        private bool BeValidDateOfBirth(DateTime? dateOfBirth)
        {
            if (dateOfBirth == null)
                return true;

            return ((TimeSpan)(DateTime.Now - dateOfBirth)).TotalDays / 365.2425 <= 120 &&
                   DateTime.Compare(DateTime.Now, (DateTime)dateOfBirth) >= 0;
        }

    }
}
