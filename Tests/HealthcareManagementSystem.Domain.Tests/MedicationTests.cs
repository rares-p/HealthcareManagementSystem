using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Domain.Tests
{
    public class MedicationTests
    {
        [Fact]
        public void CreateMedication_ValidName_Success()
        {
            // Arrange
            string medicationName = "Ibuprofen";

            // Act
            var result = Medication.Create(medicationName);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(medicationName, result.Value.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void CreateMedication_InvalidName_Failure(string invalidName)
        {
            // Act
            var result = Medication.Create(invalidName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Medication name cannot be empty!", result.Error);
        }

        [Fact]
        public void UpdateMedicationName_ValidName_Success()
        {
            // Arrange
            var medication = Medication.Create("Paracetamol").Value;

            // Act
            var result = medication.UpdateName("Aspirin");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Aspirin", medication.Name);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateMedicationName_InvalidName_Failure(string invalidName)
        {
            // Arrange
            var medication = Medication.Create("Paracetamol").Value;

            // Act
            var result = medication.UpdateName(invalidName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Medication name is required", result.Error);
            Assert.NotEqual(invalidName, medication.Name);
        }
    }
}
