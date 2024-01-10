using HealthcareManagementSystem.App.Contracts;
using HealthcareManagementSystem.App.Services.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Services
{
	public class MedicDataService : IMedicDataService
	{
		private const string RequestUri = "api/v1/medics";
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;

		public MedicDataService(HttpClient httpClient, ITokenService tokenService)
		{
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<ApiResponse<MedicDto>> CreateMedicAsync(MedicViewModel medicViewModel)
		{
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync("api/v1/Authentication/register_medic", medicViewModel);
			result.EnsureSuccessStatusCode();
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<MedicDto>>();
			response!.IsSuccess = result.IsSuccessStatusCode;
			return response!;
		}

		public Task<ApiResponse<MedicDto>> DeleteMedicAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<MedicViewModel> GetMedicByIdAsync(Guid id)
		{
			var result = await httpClient.GetAsync($"{RequestUri}/{id}");
			result.EnsureSuccessStatusCode();
			var content = await result.Content.ReadAsStringAsync();
			if (!result.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
			var medic = JsonSerializer.Deserialize<MedicViewModel>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return medic!;
		}

		public async Task<List<GetMedic>> GetMedicsAsync()
		{
			//var result = await httpClient.GetAsync(RequestUri, HttpCompletionOption.ResponseHeadersRead);
            var result = await httpClient.GetAsync(RequestUri);
            result.EnsureSuccessStatusCode();
			var content = await result.Content.ReadAsStringAsync();
			if (!result.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}
			var medics = JsonSerializer.Deserialize<GetMedicArray>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return medics!.Medics;
		}

		public Task<ApiResponse<MedicDto>> UpdateMedicAsync(UpdateMedicViewModel medicViewModel)
		{
			throw new NotImplementedException();
		}
	}
}

public class GetMedicArray
{
	public List<GetMedic> Medics { get; set; }
}