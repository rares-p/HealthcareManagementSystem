using System.ComponentModel.DataAnnotations.Schema;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class User : AuditableEntity
    {
        private User(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }

        public static Result<User> Create(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result<User>.Failure("First Name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result<User>.Failure("Last Name is required");

            if ((DateTime.Now - dateOfBirth).TotalDays / 365.2425 > 120 || DateTime.Compare(DateTime.Now, dateOfBirth) < 0)
                return Result<User>.Failure("Date of birth is invalid");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                return Result<User>.Failure("Phone number is required");

            if (phoneNumber[0] != 0 && phoneNumber.Length != 10)
                return Result<User>.Failure("Phone number is not valid");

            return Result<User>.Success(new User(firstName, lastName, dateOfBirth, phoneNumber));
        }
    }
}
