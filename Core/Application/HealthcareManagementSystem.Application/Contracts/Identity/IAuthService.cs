using HealthcareManagementSystem.Application.Models.Identity;
using HealthcareManagementSystem.Domain.Common;

namespace HealthcareManagementSystem.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<AuthResult> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);
        Task<(int, string)> Logout();
    }
}
