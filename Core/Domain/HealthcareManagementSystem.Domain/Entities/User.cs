using System.Net.Mail;
using System.Text.RegularExpressions;
using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class User : AuditableEntity
    {
        private User(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber,
            string username, string password, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
            Username = username;
            Password = password;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public static Result<User> Create(string firstName, string lastName, DateTime dateOfBirth, string phoneNumber,
            string username, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result<User>.Failure("First Name is required");

            if (string.IsNullOrWhiteSpace(lastName))
                return Result<User>.Failure("Last Name is required");

            if ((DateTime.Now - dateOfBirth).TotalDays / 365.2425 > 120 || DateTime.Compare(DateTime.Now, dateOfBirth) < 0)
                return Result<User>.Failure("Date of birth is invalid");

            if (string.IsNullOrWhiteSpace(phoneNumber))
                return Result<User>.Failure("Phone number is required");

            if (!Regex.IsMatch(phoneNumber, "0[0-9]{9}"))
                return Result<User>.Failure("Phone number is not valid");

            if (string.IsNullOrWhiteSpace(username))
                return Result<User>.Failure("Username is required");

            if (string.IsNullOrWhiteSpace(password))
                return Result<User>.Failure("Password is required");

            if (!MailAddress.TryCreate(email, out _))
                return Result<User>.Failure("Provided mail address is not valid");

            return Result<User>.Success(new User(firstName, lastName, dateOfBirth, phoneNumber, username, password, email));
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

        public Result<User> UpdatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return Result<User>.Failure("Phone number is required");
            PhoneNumber = phoneNumber;
            return Result<User>.Success(this);
        }

        public Result<User> UpdateUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Result<User>.Failure("Username is required");
            Username = username;
            return Result<User>.Success(this);
        }

        public Result<User> UpdatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Result<User>.Failure("Password is required");
            Password = password;
            return Result<User>.Success(this);
        }
        public Result<User> UpdateEmail(string email)
        {
            if (!MailAddress.TryCreate(email, out _))
                return Result<User>.Failure("Provided mail address is not valid");
            Email = email;
            return Result<User>.Success(this);
        }
    }
}
