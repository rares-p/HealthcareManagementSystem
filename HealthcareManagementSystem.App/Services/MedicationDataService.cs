using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.ViewModels;
using System.Text.Json;
using HealthcareManagementSystem.App.Services.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HealthcareManagementSystem.App.Services
{
    public class MedicationDataService : IMedicationDataService
    {
        private const string RequestUri = "api/v1/medication";
        private readonly HttpClient httpClient;
        private readonly ITokenService tokenService;

        public MedicationDataService(HttpClient httpClient, ITokenService tokenService)
        {
            this.httpClient = httpClient;
            this.tokenService = tokenService;
        }

        public async Task<List<MedicationViewModel>> GetMedicationsAsync()
        {
            var result = await httpClient.GetAsync(RequestUri);
            result.EnsureSuccessStatusCode();
            var content = await result.Content.ReadAsStringAsync();
            if (!result.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var medications = JsonSerializer.Deserialize<Dictionary<string, List<MedicationViewModel>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Values.FirstOrDefault();
            return medications!;
        }

        public async Task<ApiResponse<MedicationDto>> CreateMedicationAsync(MedicationViewModel medicationViewModel)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.PostAsJsonAsync(RequestUri, medicationViewModel);
            result.EnsureSuccessStatusCode();
            var response = await result.Content.ReadFromJsonAsync<ApiResponse<MedicationDto>>();
            response!.IsSuccess = result.IsSuccessStatusCode;
            return response!;
        }

        public async Task DeleteMedicationAsync(Guid id)
        {
            httpClient.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
            var result = await httpClient.DeleteAsync($"{RequestUri}/{id}");
        }
    }
}
