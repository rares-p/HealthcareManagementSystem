using HealthcareManagementSystem.Application.Persistence;
using HealthcareManagementSystem.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HealthcareManagementSystem.Infrastructure
{
    public static class InfrastructureRegistrationDi
    {
        public static IServiceCollection AddInfrastructureToDi(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HealthcareManagementSystemDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString
                        ("HealthcareManagementSystemDbConnection"),
                builder => builder.MigrationsAssembly("API")));
            services.AddScoped
            (typeof(IAsyncRepository<>),
                typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMedicRepository, MedicRepository>();
            services.AddScoped<IMedicationReminderRepository, MedicationReminderRepository>();
            services.AddScoped<IExaminationRepository, ExaminationRepository>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();

            return services;
        }
    }

}
