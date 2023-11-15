using FluentValidation;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(user => user.Id).NotNull().WithMessage("Id cannot be null");
        }
    }
}
