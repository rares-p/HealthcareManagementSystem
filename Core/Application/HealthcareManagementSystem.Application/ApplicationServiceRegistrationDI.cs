using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace HealthcareManagementSystem.Application
{
    public static class ApplicationServiceRegistrationDI
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR
            (
                cfg => cfg.RegisterServicesFromAssembly(

                    Assembly.GetExecutingAssembly())
            );
        }
    }
}
