using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Contracts
{
    public interface IMedicationDataService
    {
        Task<List<MedicationViewModel>> GetMedicationsAsync();
        Task<ApiResponse<MedicationDto>> CreateMedicationAsync(MedicationViewModel medicationViewModel);
        Task DeleteMedicationAsync(Guid id);
    }
}
