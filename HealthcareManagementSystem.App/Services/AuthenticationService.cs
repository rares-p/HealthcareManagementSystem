using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.ViewModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace HealthcareManagementSystem.App.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;
		private const string RequestUri = "api/v1/users";

		public AuthenticationService(HttpClient httpClient, ITokenService tokenService)
		{
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<UserViewModel> GetCurrentUser()
		{
			//var result = await httpClient.GetAsync($"{RequestUri}/username/{username}");
			//result.EnsureSuccessStatusCode();
			//var content = await result.Content.ReadAsStringAsync();
			//if (!result.IsSuccessStatusCode)
			//{
			//	throw new ApplicationException(content);
			//}
			//var user = JsonSerializer.Deserialize<UserViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			//return user!;
			throw new NotImplementedException();
		}

		public async Task Login(LoginViewModel loginRequest)
		{
			var response = await httpClient.PostAsJsonAsync("api/v1/authentication/login", loginRequest);
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				throw new Exception(await response.Content.ReadAsStringAsync());
			}
			response.EnsureSuccessStatusCode();
			var token = await response.Content.ReadAsStringAsync();
			await tokenService.SetTokenAsync(token);
		}

		public async Task Logout()
		{
			await tokenService.RemoveTokenAsync();
			var result = await httpClient.PostAsync("api/v1/authentication/logout", null);
			result.EnsureSuccessStatusCode();
		}

		public async Task Register(RegisterViewModel registerRequest)
		{
			var result = await httpClient.PostAsJsonAsync("api/v1/authentication/register", registerRequest);
			if (result.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				throw new Exception(await result.Content.ReadAsStringAsync());
			}
			result.EnsureSuccessStatusCode();
		}
	}
}
