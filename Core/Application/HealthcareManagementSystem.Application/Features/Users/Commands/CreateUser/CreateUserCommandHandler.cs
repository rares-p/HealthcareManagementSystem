﻿using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using MediatR;

namespace HealthcareManagementSystem.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResponse>
    {
        private readonly IUserRepository _repository;

        public CreateUserCommandHandler(IUserRepository repository)
        {
            this._repository = repository;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validatorResult.IsValid)
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };

            var user = User.Create(request.FirstName, request.LastName, request.DateOfBirth, request.PhoneNumber, 
                request.Username, request.Password, request.Email);
            if (!user.IsSuccess)
            {
                return new CreateUserCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { user.Error }
                };
            }

            await _repository.AddAsync(user.Value);

            return new CreateUserCommandResponse
            {
                Success = true,
                User = new CreateUserDto
                {
                    Id = user.Value.Id,
                    FirstName = user.Value.FirstName,
                    LastName = user.Value.LastName,
                    Email = user.Value.Email,
                    Username = user.Value.Username,
                    DateOfBirth = user.Value.DateOfBirth,
                    PhoneNumber = user.Value.PhoneNumber
                }
            };
        }
    }
}
