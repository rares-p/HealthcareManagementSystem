using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Services
{
	public class MedicationReminderDataService : IMedicationReminderDataService
	{
		private const string RequestUri = "api/v1/medicationreminder";
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;

		public MedicationReminderDataService(HttpClient httpClient, ITokenService tokenService)
		{
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<List<MedicationReminderViewModel>> GetMedicationRemindersAsync()
		{
			var result = await httpClient.GetAsync(RequestUri);
			result.EnsureSuccessStatusCode();
			var content = await result.Content.ReadAsStringAsync();
			if (!result.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
			var medications = JsonSerializer.Deserialize<Dictionary<string, List<MedicationReminderViewModel>>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }).Values.FirstOrDefault();
			return medications!;
		}

		public async Task<ApiResponse<MedicationReminderDto>> CreateMedicationReminderAsync(MedicationReminderViewModel medicationViewModel)
		{
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync(RequestUri, medicationViewModel);
			result.EnsureSuccessStatusCode();
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<MedicationReminderDto>>();
			response!.IsSuccess = result.IsSuccessStatusCode;
			return response!;
		}

		public async Task UpdateMedicationReminderAsync(MedicationReminderDto updateMedicationViewModel)
		{
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PutAsJsonAsync(RequestUri, updateMedicationViewModel);
		}

	}
}
