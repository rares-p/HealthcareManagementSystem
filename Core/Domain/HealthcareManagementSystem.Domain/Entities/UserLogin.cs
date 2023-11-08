using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class UserLogin : AuditableEntity
    {
        private UserLogin(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public Guid UserId { get; private set; }
        public string Username { get; private set;}
        public string Password { get; private set;}
        public string Email { get; private set;}

        public static Result<UserLogin> Create(string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result<UserLogin>.Failure("Username is required");

            if (string.IsNullOrWhiteSpace(password))
                return Result<UserLogin>.Failure("Password is required");

            if (string.IsNullOrWhiteSpace(email))
                return Result<UserLogin>.Failure("Email is required");

            return Result<UserLogin>.Success(new UserLogin(username, password, email));
        }

    }
}
