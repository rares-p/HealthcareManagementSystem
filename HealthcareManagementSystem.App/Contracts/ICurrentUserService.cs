using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Contracts
{
	public interface ICurrentUserService
	{
		Task<UserViewModel> GetCurrentUser();
		Task<Guid> GetCurrentUserId();
	}
}
