using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Domain.Tests
{
    public class ExaminationTests
    {
        [Fact]
        public void CreateExamination_ValidData_Success()
        {
            // Arrange
            DateTime examinationDate = DateTime.Now;
            Department department = Department.Cardiology;
            Guid userId = Guid.NewGuid();
            Guid medicId = Guid.NewGuid();

            // Act
            var result = Examination.Create(examinationDate, department, userId, medicId);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(examinationDate, result.Value.Date);
            Assert.Equal(department, result.Value.Department);
            Assert.Equal(userId, result.Value.UserId);
            Assert.Equal(medicId, result.Value.MedicId);
        }

        [Fact]
        public void CreateExamination_InvalidDate_Failure()
        {
            // Arrange
            DateTime invalidDate = DateTime.MinValue; // Invalid date
            Department department = Department.Cardiology;
            Guid userId = Guid.NewGuid();
            Guid medicId = Guid.NewGuid();

            // Act
            var result = Examination.Create(invalidDate, department, userId, medicId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid date", result.Error);
        }

        [Fact]
        public void CreateExamination_InvalidDepartment_Failure()
        {
            // Arrange
            DateTime examinationDate = DateTime.Now;
            Department invalidDepartment = (Department)99; // Invalid department
            Guid userId = Guid.NewGuid();
            Guid medicId = Guid.NewGuid();

            // Act
            var result = Examination.Create(examinationDate, invalidDepartment, userId, medicId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Invalid department", result.Error);
        }
    }
}
