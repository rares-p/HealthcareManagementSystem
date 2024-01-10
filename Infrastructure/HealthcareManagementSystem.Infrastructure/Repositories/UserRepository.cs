using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Common;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(HealthcareManagementSystemDbContext context) : base(context)
        {

        }

        public async Task<Result<User>> GetUserByUsernameAsync(string username)
        {
            var result = await Context.Set<User>().FindAsync(username);
            if (result == null)
            {
                return Result<User>.Failure($"Entity with username {username} not found");
            }
            return Result<User>.Success(result);
        }
    }
}
