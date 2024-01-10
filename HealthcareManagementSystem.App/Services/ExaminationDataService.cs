using System.Net.Http.Headers;
using System.Net.Http.Json;
using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;
using System.Text.Json;

namespace HealthcareManagementSystem.App.Services
{
	public class ExaminationDataService : IExaminationDataService
	{
        private const string RequestUri = "api/v1/examination";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public ExaminationDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }
        public async Task<List<ExaminationViewModel>> GetExaminationsAsync()
        {
            var result = await httpClient.GetAsync(RequestUri);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var examinations = JsonSerializer.Deserialize<Dictionary<string, List<ExaminationViewModel>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Values.FirstOrDefault();
            return examinations!;
        }

        public async Task<ApiResponse<ExaminationViewModel>> CreateExaminationAsync(CreateExaminationViewModel examinationViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, examinationViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<ExaminationViewModel>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }
    }
}
