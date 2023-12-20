using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Domain.Tests
{
    public class MedicationReminderTests
    {
        [Fact]
        public void CreateMedicationReminder_ValidData_Success()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1); // Tomorrow
            DateTime endDate = DateTime.Today.AddDays(10); // Ten days later
            uint dayInterval = 1;
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, hourList);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(dosage, result.Value.Dosage);
            Assert.Equal(startDate, result.Value.StartDate);
            Assert.Equal(endDate, result.Value.EndDate);
            Assert.Equal(dayInterval, result.Value.DayInterval);
            Assert.Equal(hourList, result.Value.HourList);
        }

        [Theory]
        [InlineData(0, "Medication dosage is not valid!")]
        [InlineData(11, "Medication dosage is not valid!")]
        public void CreateMedicationReminder_InvalidDosage_Failure(uint invalidDosage, string expectedErrorMessage)
        {
            // Arrange
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), invalidDosage, startDate, endDate, dayInterval, hourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedErrorMessage, result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_StartDateAfterEndDate_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(10); // Future date
            DateTime endDate = DateTime.Today.AddDays(1); // Past date
            uint dayInterval = 1;
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, hourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Start Date must be before End Date!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_InvalidDayInterval_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 0; // Invalid day interval
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, hourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Day Interval must be higher or equal to 1!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_EmptyHourList_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> emptyHourList = new List<float>(); // Empty hour list

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, emptyHourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Hour List is empty!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_InvalidHourInList_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> invalidHourList = new List<float> { 8.30f, 12.15f, 25.0f }; // Invalid hour (25.0)

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, invalidHourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("All hours must be between 0:00 and 23:59!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_InvalidMinutesInHourList_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> invalidMinuteHourList = new List<float> { 8.75f, 12.15f, 18.45f }; // Invalid minute in hour (8.75)

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, invalidMinuteHourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Minutes in an hour must be between 0 and 59!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_ValidMinuteInHourList_Success()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> validMinuteHourList = new List<float> { 8.30f, 12.59f, 18.45f }; // Valid minute in hour (12.59)

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, validMinuteHourList);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }

        [Fact]
        public void CreateMedicationReminder_InvalidStartAndEndDate_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1); // Tomorrow
            DateTime endDate = DateTime.Today; // Today (invalid)
            uint dayInterval = 1;
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, hourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Start Date must be before End Date!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_InvalidHourRangeInList_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> invalidHourList = new List<float> { -1.0f, 12.15f, 24.0f }; // Invalid hours: -1.0, 24.0

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, invalidHourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("All hours must be between 0:00 and 23:59!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_InvalidHourMinuteRangeInList_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> invalidMinuteHourList = new List<float> { 8.30f, 12.75f, 18.45f }; // Invalid minute in hour (12.75)

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, invalidMinuteHourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Minutes in an hour must be between 0 and 59!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_NullHourList_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> nullHourList = null; // Null hour list

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, nullHourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Hour List is empty!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_StartDateEqualsEndDate_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today;
            DateTime endDate = DateTime.Today;
            uint dayInterval = 1;
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, hourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Start Date must be before End Date!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_InvalidStartDate_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.Today.AddDays(10);
            uint dayInterval = 1;
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, endDate, startDate, dayInterval, hourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Start Date must be before End Date!", result.Error);
        }

        [Fact]
        public void CreateMedicationReminder_EmptyEndDate_Failure()
        {
            // Arrange
            uint dosage = 5;
            DateTime startDate = DateTime.Today.AddDays(1);
            DateTime endDate = DateTime.MinValue; // Invalid end date
            uint dayInterval = 1;
            List<float> hourList = new List<float> { 8.30f, 12.15f, 18.45f };

            // Act
            var result = MedicationReminder.Create(Guid.NewGuid(), Guid.NewGuid(), dosage, startDate, endDate, dayInterval, hourList);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Start Date must be before End Date!", result.Error);
        }
    }
}
