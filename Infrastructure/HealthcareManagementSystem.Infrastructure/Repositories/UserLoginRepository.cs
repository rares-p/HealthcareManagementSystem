using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class UserLoginRepository : BaseRepository<UserLogin>, IAsyncRepository<UserLogin>
    {
        public UserLoginRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
