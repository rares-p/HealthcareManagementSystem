using System.Net.Mail;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Data;

namespace HealthcareManagementSystem.Domain.Entities
{
    public class Medic : AuditableEntity
    {
        private Medic(string firstName, string lastName, Department department, string email)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            Email = email;
        }
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Department Department { get; private set; }
        public string Email { get; private set; }

        public static Result<Medic> Create(string firstName, string lastName, Department department, string email)
        {
            if(string.IsNullOrWhiteSpace(firstName))
                return Result<Medic>.Failure("First Name is required");

            if(string.IsNullOrWhiteSpace(lastName))
                return Result<Medic>.Failure("Last Name is required");

            if(!MailAddress.TryCreate(email, out _))
                return Result<Medic>.Failure("Provided mail address is not valid");

            return Result<Medic>.Success(new Medic(firstName, lastName, department, email));
        }
    }
}
