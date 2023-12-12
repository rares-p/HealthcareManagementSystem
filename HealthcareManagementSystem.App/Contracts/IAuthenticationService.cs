using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Contracts
{
    public interface IAuthenticationService
    {
		Task Login(LoginViewModel loginRequest);
		Task Register(RegisterViewModel registerRequest);
		Task Logout();
	}
}
