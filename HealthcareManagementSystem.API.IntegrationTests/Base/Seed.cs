using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Domain.Entities;
using HealthcareManagementSystem.Infrastructure;

namespace HealthcareManagementSystem.API.IntegrationTests.Base
{
    public class Seed
    {
        public static void InitializeDbForTests(HealthcareManagementSystemDbContext context)
        {
            var medications = new List<Medication>
            {
                Medication.Create("Diphenhydramine").Value,
                Medication.Create("Ibuprofen").Value,
                Medication.Create("Acetaminophen").Value,
                Medication.Create("Acetylsalicylic acid").Value,
                Medication.Create("Diclofenac").Value,
            };
            context.Medications.AddRange(medications);

            var users = new List<User>
            {
                User.Create("Test", "1", DateTime.Now, "testauth").Value,
                User.Create("Test", "2", DateTime.Now, "testauth").Value,
                User.Create("Test", "3", DateTime.Now, "testauth").Value,
            };
            context.Users.AddRange(users);

            var medicationReminders = new List<MedicationReminder>
            {
                MedicationReminder.Create(users[0].Id, medications[0].Id, 8u, DateTime.Now + TimeSpan.MaxValue / 4, DateTime.MaxValue, 3u,
                    new List<float> { 1f, 3f }).Value,
                MedicationReminder.Create(users[1].Id, medications[2].Id, 3u, DateTime.Now, DateTime.MaxValue, 2u,
                    new List<float> { 2f }).Value,
            };
            context.MedicationReminders.AddRange(medicationReminders);

            var medics = new List<Medic>
            {
                Medic.Create("Test", "1", Department.Anesthesiology, "testauth").Value,
                Medic.Create("Test", "2", Department.Anesthesiology, "testauth").Value,
                Medic.Create("Test", "3", Department.Anesthesiology, "testauth").Value,
                Medic.Create("Test", "4", Department.Anesthesiology, "testauth").Value,
            };
            context.Medics.AddRange(medics);

            context.SaveChanges();
        }
    }
}
