using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Domain.Tests
{
    public class MedicTests
    {
        [Fact]
        public void CreateMedic_ValidData_Success()
        {
            // Arrange
            string firstName = "Dr. John";
            string lastName = "Doe";
            Department department = Department.Cardiology;
            string authDataId = "someAuthDataId";

            // Act
            var result = Medic.Create(firstName, lastName, department, authDataId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(firstName, result.Value.FirstName);
            Assert.Equal(lastName, result.Value.LastName);
            Assert.Equal(department, result.Value.Department);
            Assert.Equal(authDataId, result.Value.AuthDataId);
        }

        [Fact]
        public void CreateMedic_InvalidDepartment_Failure()
        {
            // Arrange
            string firstName = "Dr. John";
            string lastName = "Doe";
            Department invalidDepartment = (Department)99; // Assigning an invalid department value
            string authDataId = "someAuthDataId";

            // Act
            var result = Medic.Create(firstName, lastName, invalidDepartment, authDataId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid department", result.Error);
        }

        [Theory]
        [InlineData("", "Doe", Department.Neurology, "First Name is required")]
        [InlineData("Dr. John", "", Department.Psychiatry, "Last Name is required")]
        public void CreateMedic_InvalidData_Failure(string firstName, string lastName, Department department, string expectedErrorMessage)
        {
            // Act
            var result = Medic.Create(firstName, lastName, department, "someAuthDataId");

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Error);
        }

        [Fact]
        public void UpdateFirstName_ValidData_Success()
        {
            // Arrange
            var medic = Medic.Create("Dr. John", "Doe", Department.Cardiology, "someAuthDataId").Value;

            // Act
            var result = medic.UpdateFirstName("Dr. Jane");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Dr. Jane", medic.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateFirstName_InvalidData_Failure(string newFirstName)
        {
            // Arrange
            var medic = Medic.Create("Dr. John", "Doe", Department.Cardiology, "someAuthDataId").Value;

            // Act
            var result = medic.UpdateFirstName(newFirstName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("First Name is required", result.Error);
            Assert.NotEqual(newFirstName, medic.FirstName);
        }

        [Fact]
        public void UpdateLastName_ValidData_Success()
        {
            // Arrange
            var medic = Medic.Create("Dr. John", "Doe", Department.Cardiology, "someAuthDataId").Value;

            // Act
            var result = medic.UpdateLastName("Dr. Smith");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Dr. Smith", medic.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateLastName_InvalidData_Failure(string newLastName)
        {
            // Arrange
            var medic = Medic.Create("Dr. John", "Doe", Department.Cardiology, "someAuthDataId").Value;

            // Act
            var result = medic.UpdateLastName(newLastName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Last Name is required", result.Error);
            Assert.NotEqual(newLastName, medic.LastName);
        }

        [Fact]
        public void UpdateDepartment_ValidData_Success()
        {
            // Arrange
            var medic = Medic.Create("Dr. John", "Doe", Department.Cardiology, "someAuthDataId").Value;
            Department newDepartment = Department.Oncology;

            // Act
            var result = medic.UpdateDepartment(newDepartment);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newDepartment, medic.Department);
        }

        [Fact]
        public void UpdateDepartment_InvalidDepartment_Failure()
        {
            // Arrange
            var medic = Medic.Create("Dr. John", "Doe", Department.Cardiology, "someAuthDataId").Value;
            Department invalidDepartment = (Department)99; // Assigning an invalid department value

            // Act
            var result = medic.UpdateDepartment(invalidDepartment);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid department", result.Error);
            Assert.NotEqual(invalidDepartment, medic.Department);
        }

    }
}
