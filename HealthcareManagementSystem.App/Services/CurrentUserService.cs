using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.ViewModels;
using System.Security.Claims;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace HealthcareManagementSystem.App.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		private readonly HttpClient _httpClient;
		private readonly ITokenService _tokenService;

		public CurrentUserService(HttpClient httpClient, ITokenService tokenService)
		{
			_httpClient = httpClient;
			_tokenService = tokenService;
		}

		public async Task<UserViewModel> GetCurrentUser()
		{
			var handler = new JwtSecurityTokenHandler();
			var token = await _tokenService.GetTokenAsync();
			var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

			string username = jsonToken.Claims.FirstOrDefault(c => c.Type == "unique_name")?.Value;

			var result = await _httpClient.GetAsync($"api/v1/users/username/{username}");
			result.EnsureSuccessStatusCode();
			var content = await result.Content.ReadAsStringAsync();
			if (!result.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
			var user = JsonSerializer.Deserialize<UserViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return user!;
		}

		public async Task<Guid> GetCurrentUserId()
		{
			var handler = new JwtSecurityTokenHandler();
			var token = await _tokenService.GetTokenAsync();
			var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

			var idClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "oid");

            if (idClaim != null && Guid.TryParse(idClaim.Value, out var userId))
            {
                return userId;
            }

			return Guid.Empty;
        }
    }
}
