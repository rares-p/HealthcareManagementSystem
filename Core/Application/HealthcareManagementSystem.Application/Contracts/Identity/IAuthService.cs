using HealthcareManagementSystem.Application.Models.Identity;

namespace HealthcareManagementSystem.Application.Contracts.Identity
{
    public interface IAuthService
    {
        Task<(int, string)> Registration(RegistrationModel model, string role);
        Task<(int, string)> Login(LoginModel model);

        Task<(int, string)> Logout();
    }
}
