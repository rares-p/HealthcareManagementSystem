using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
