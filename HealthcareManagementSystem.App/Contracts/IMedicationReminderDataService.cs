using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Contracts
{
	public interface IMedicationReminderDataService
	{
		Task<List<MedicationReminderViewModel>> GetMedicationRemindersAsync();
		Task<ApiResponse<MedicationReminderDto>> CreateMedicationReminderAsync(MedicationReminderViewModel medicationReminderViewModel);
		Task UpdateMedicationReminderAsync(MedicationReminderDto medicationReminderViewModel);
	}
}
