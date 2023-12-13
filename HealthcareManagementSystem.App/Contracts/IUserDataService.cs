using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;
using HealthcareManagementSystem.Domain.Entities;

namespace HealthcareManagementSystem.App.Contracts
{
    public interface IUserDataService
    {
	    Task<List<UserViewModel>> GetUsersAsync();
	    Task<UserViewModel> GetUserByIdAsync(Guid id);
	    Task<ApiResponse<UserDto>> CreateUserAsync(UserViewModel userViewModel);
	    Task<ApiResponse<UserDto>> UpdateUserAsync(UserViewModel userViewModel);
	    Task<ApiResponse<UserDto>> DeleteUserAsync(Guid id);
	}
}
