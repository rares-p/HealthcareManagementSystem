using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Data;
using HealthcareManagementSystem.Domain.Entities;
using HealthcareManagementSystem.Infrastructure;
using HealthcareManagementSystem.Infrastructure.Repositories;
using NSubstitute;

namespace HealthcareManagementSystem.Application.Tests
{
    public class RepositoryMocks
    {
        public static IUserRepository GetUserRepository()
        {
            var users = new List<User>
            {
                User.Create("John", "Doe", new DateTime(1990, 5, 15), "auth123").Value,
                User.Create("John2", "Doe", new DateTime(1990, 5, 15), "auth123").Value,
                User.Create("John3", "Doe", new DateTime(1990, 5, 15), "auth123").Value
            };

            var userRepository = Substitute.For<IUserRepository>();
            userRepository.GetAllAsync().Returns(Result<IReadOnlyList<User>>.Success(users));

            return userRepository;
        }

        public static IMedicRepository GetMedicRepository()
        {
            var medics = new List<Medic>
            {
                Medic.Create("John", "Doe", Department.Cardiology, "auth123").Value,
                Medic.Create("John2", "Doe", Department.Neurology, "auth456").Value,
                Medic.Create("John3", "Doe", Department.Pediatrics, "auth789").Value
            };

            var medicRepository = Substitute.For<IMedicRepository>();
            medicRepository.GetAllAsync().Returns(Result<IReadOnlyList<Medic>>.Success(medics));

            return medicRepository;
        }

        [Fact]
        public async Task AddUserAsync_ValidUser_Success()
        {
            // Arrange
            var dbContext = Substitute.For<HealthcareManagementSystemDbContext>();
            var repository = new BaseRepository<User>(dbContext);
            var user = User.Create("John", "Doe", new DateTime(1990, 5, 15), "auth123");

            dbContext.Set<User>().Returns(Substitute.For<IQueryable<User>>());
            repository.AddAsync(Arg.Any<User>()).Returns(Task.FromResult(user));

            // Act
            var result = await repository.AddAsync(user.Value);

            // Assert
            await dbContext.Set<User>().Received(1).AddAsync(Arg.Any<User>());
            await dbContext.Received(1).SaveChangesAsync();
            Assert.True(result.IsSuccess);
            Assert.Equal(user.Value, result.Value);
        }
    }
}
