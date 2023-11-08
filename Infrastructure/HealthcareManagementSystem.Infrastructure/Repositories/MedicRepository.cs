﻿using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthcareManagementSystem.Infrastructure.Repositories
{
    public class MedicRepository : BaseRepository<Medic>, IAsyncRepository<Medic>
    {
        public MedicRepository(HealthcareManagementSystemDbContext context) : base(context)
        {
        }
    }
}
