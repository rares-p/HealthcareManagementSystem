using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Application.Persistence
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<Result<User>> GetByUsernameAsync(string username);
    }
}
