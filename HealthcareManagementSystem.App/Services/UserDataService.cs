using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Services
{
	public class UserDataService : IUserDataService
	{
		private const string RequestUri = "api/v1/users";
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;

		public UserDataService(HttpClient httpClient, ITokenService tokenService)
		{
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<ApiResponse<UserDto>> CreateUserAsync(UserViewModel userViewModel)
		{
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync(RequestUri, userViewModel);
			result.EnsureSuccessStatusCode();
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
			response!.IsSuccess = result.IsSuccessStatusCode;
			return response!;
		}

		public Task<ApiResponse<UserDto>> DeleteUserAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<UserViewModel> GetUserByIdAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<UserViewModel>> GetUsersAsync()
		{
			var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
			result.EnsureSuccessStatusCode();
			var content = await result.Content.ReadAsStringAsync();
			if (!result.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
			var users = JsonSerializer.Deserialize<List<UserViewModel>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return users!;
		}

		public Task<ApiResponse<UserDto>> UpdateUserAsync(UserViewModel userViewModel)
		{
			throw new NotImplementedException();
		}
	}
}
