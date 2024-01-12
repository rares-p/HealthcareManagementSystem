using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

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
        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            //var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            var result = await httpClient.GetAsync(RequestUri);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var users = JsonSerializer.Deserialize<Dictionary<string, List<UserViewModel>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Values.FirstOrDefault();
            return users!;
        }

		
	}
}
