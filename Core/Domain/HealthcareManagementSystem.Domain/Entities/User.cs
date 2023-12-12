using System.Net.Mail;
using System.Text.RegularExpressions;
using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class User : AuditableEntity
    {
        private User(string firstName, string lastName, DateTime dateOfBirth, string authDataId)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            AuthDataId = authDataId;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string AuthDataId { get; private set; }

        public static Result<User> Create(string firstName, string lastName, DateTime dateOfBirth, string authDataId)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result<User>.Failure("First Name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result<User>.Failure("Last Name is required");

            if ((DateTime.Now - dateOfBirth).TotalDays / 365.2425 > 120 || DateTime.Compare(DateTime.Now, dateOfBirth) < 0)
                return Result<User>.Failure("Date of birth is invalid");

            return Result<User>.Success(new User(firstName, lastName, dateOfBirth, authDataId));
        }

        public Result<User> UpdateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result<User>.Failure("First Name is required");
            FirstName = firstName;
            return Result<User>.Success(this);
        }

        public Result<User> UpdateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return Result<User>.Failure("Last Name is required");
            LastName = lastName;
            return Result<User>.Success(this);
        }

        public Result<User> UpdateDateOfBirth(DateTime dateOfBirth)
        {
            if ((DateTime.Now - dateOfBirth).TotalDays / 365.2425 > 120 || DateTime.Compare(DateTime.Now, dateOfBirth) < 0)
                return Result<User>.Failure("Date of birth is invalid");
            DateOfBirth = dateOfBirth;
            return Result<User>.Success(this);
        }
    }
}
