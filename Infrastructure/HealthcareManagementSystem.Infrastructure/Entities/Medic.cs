using System.Net.Mail;
using HealthcareManagementSystem.Infrastructure.Data;

namespace HealthcareManagementSystem.Infrastructure.Entities
{
    public class Medic
    {
        private Medic(string firstName, string lastName, Department department, MailAddress mail)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Department = department;
            Mail = mail;
        }
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Department Department { get; private set; }
        public MailAddress Mail { get; private set; }
    }
}
