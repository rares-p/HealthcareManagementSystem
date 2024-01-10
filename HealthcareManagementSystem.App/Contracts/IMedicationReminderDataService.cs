using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Contracts
{
	public interface IMedicationReminderDataService
	{
        Task<MedicationReminderDto> GetMedicationReminderByIdAsync(Guid id);
        Task<List<MedicationReminderDto>> GetMedicationRemindersAsync();
		Task<ApiResponse<MedicationReminderDto>> CreateMedicationReminderAsync(MedicationReminderViewModel medicationReminderViewModel);
		Task UpdateMedicationReminderAsync(MedicationReminderDto medicationReminderViewModel);
	}
}
