using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse>
    {
        private readonly IUserRepository _userRepository;

        public DeleteCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new DeleteUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var result = await _userRepository.DeleteAsync(request.Id);

            if (!result.IsSuccess)
                return new DeleteUserCommandResponse()
                {
                    Success = false,
                    Message = result.Error
                };

            return new DeleteUserCommandResponse()
            {
                Success = true,
                Message = $"User with id {request.Id} deleted successfully"
            };
        }
    }
}
