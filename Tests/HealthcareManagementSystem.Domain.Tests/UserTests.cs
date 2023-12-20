using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Domain.Tests
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_ValidData_Success()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            DateTime dateOfBirth = new DateTime(1990, 5, 15);
            string authDataId = "someAuthDataId";

            // Act
            var result = User.Create(firstName, lastName, dateOfBirth, authDataId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(firstName, result.Value.FirstName);
            Assert.Equal(lastName, result.Value.LastName);
            Assert.Equal(dateOfBirth, result.Value.DateOfBirth);
            Assert.Equal(authDataId, result.Value.AuthDataId);
        }

        [Theory]
        [InlineData("", "Doe", "someAuthDataId", "First Name is required")]
        [InlineData("John", "", "someAuthDataId", "Last Name is required")]
        public void CreateUser_InvalidData_Failure(string firstName, string lastName, string authDataId, string expectedErrorMessage)
        {
            // Arrange
            DateTime dateOfBirth = new DateTime(1990, 5, 15);

            // Act
            var result = User.Create(firstName, lastName, dateOfBirth, authDataId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Error);
        }

        [Fact]
        public void CreateUser_InvalidDateOfBirth_Failure()
        {
            // Arrange
            string firstName = "John";
            string lastName = "Doe";
            DateTime futureDate = DateTime.Now.AddDays(1);
            string authDataId = "someAuthDataId";

            // Act
            var result = User.Create(firstName, lastName, futureDate, authDataId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Date of birth is invalid", result.Error);
        }

        [Fact]
        public void UpdateFirstName_ValidData_Success()
        {
            // Arrange
            var user = User.Create("John", "Doe", new DateTime(1990, 5, 15), "someAuthDataId").Value;

            // Act
            var result = user.UpdateFirstName("Jane");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Jane", user.FirstName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateFirstName_InvalidData_Failure(string newFirstName)
        {
            // Arrange
            var user = User.Create("John", "Doe", new DateTime(1990, 5, 15), "someAuthDataId").Value;

            // Act
            var result = user.UpdateFirstName(newFirstName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("First Name is required", result.Error);
            Assert.NotEqual(newFirstName, user.FirstName);
        }

        [Fact]
        public void UpdateLastName_ValidData_Success()
        {
            // Arrange
            var user = User.Create("John", "Doe", new DateTime(1990, 5, 15), "someAuthDataId").Value;

            // Act
            var result = user.UpdateLastName("Smith");

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Smith", user.LastName);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void UpdateLastName_InvalidData_Failure(string newLastName)
        {
            // Arrange
            var user = User.Create("John", "Doe", new DateTime(1990, 5, 15), "someAuthDataId").Value;

            // Act
            var result = user.UpdateLastName(newLastName);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Last Name is required", result.Error);
            Assert.NotEqual(newLastName, user.LastName);
        }

        [Fact]
        public void UpdateDateOfBirth_ValidData_Success()
        {
            // Arrange
            var user = User.Create("John", "Doe", new DateTime(1990, 5, 15), "someAuthDataId").Value;
            DateTime newDateOfBirth = new DateTime(1985, 10, 20);

            // Act
            var result = user.UpdateDateOfBirth(newDateOfBirth);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newDateOfBirth, user.DateOfBirth);
        }

        [Fact]
        public void UpdateDateOfBirth_InvalidData_Failure()
        {
            // Arrange
            var user = User.Create("John", "Doe", new DateTime(1990, 5, 15), "someAuthDataId").Value;
            DateTime futureDate = DateTime.Now.AddDays(1);

            // Act
            var result = user.UpdateDateOfBirth(futureDate);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Date of birth is invalid", result.Error);
            Assert.NotEqual(futureDate, user.DateOfBirth);
        }
    }
}
