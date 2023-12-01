using HealthcareManagementSystem.Application.Persistence;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            this._repository = repository;
        }
        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new UpdateUserCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var user = await _repository.FindByIdAsync((Guid)request.Id!);
            if (!user.IsSuccess)
                return new UpdateUserCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = new() { user.Error }
                };

            if (request.FirstName != null)
            {
                var result = user.Value.UpdateFirstName(request.FirstName);
                if(!result.IsSuccess)
                    return new UpdateUserCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.LastName != null)
            {
                var result = user.Value.UpdateLastName(request.LastName);
                if (!result.IsSuccess)
                    return new UpdateUserCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.DateOfBirth != null)
            {
                var result = user.Value.UpdateDateOfBirth((DateTime)request.DateOfBirth);
                if (!result.IsSuccess)
                    return new UpdateUserCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.Username != null)
            {
                var result = user.Value.UpdateUsername(request.Username);
                if (!result.IsSuccess)
                    return new UpdateUserCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.PhoneNumber != null)
            {
                var result = user.Value.UpdatePhoneNumber(request.PhoneNumber);
                if (!result.IsSuccess)
                    return new UpdateUserCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.Email != null)
            {
                var result = user.Value.UpdateEmail(request.Email);
                if (!result.IsSuccess)
                    return new UpdateUserCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            if (request.Password != null)
            {
                var result = user.Value.UpdatePassword(request.Password);
                if (!result.IsSuccess)
                    return new UpdateUserCommandResponse()
                    {
                        Success = false,
                        ValidationsErrors = new() { result.Error }
                    };
            }

            try
            {
                var updateResult = await _repository.UpdateAsync(user.Value);
            }
            catch (Exception e)
            {
                return new UpdateUserCommandResponse()
                {
                    Success = false,
                    ValidationsErrors = new() { "An internal error occurred. Please try again later" }
                };
            }

            return new UpdateUserCommandResponse()
            {
                Success = true,
                User = new UserDto()
                {
                    Id = user.Value.Id,
                    FirstName = user.Value.FirstName,
                    LastName = user.Value.LastName,
                    Email = user.Value.Email,
                    PhoneNumber = user.Value.PhoneNumber,
                    Username = user.Value.Username,
                    DateOfBirth = user.Value.DateOfBirth
                }
            };
        }
    }
}
