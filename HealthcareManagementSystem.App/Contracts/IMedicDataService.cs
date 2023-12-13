using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Contracts
{
    public interface IMedicDataService
    {
        Task<List<MedicViewModel>> GetMedicsAsync();
        Task<MedicViewModel> GetMedicByIdAsync(Guid id);
        Task<ApiResponse<MedicDto>> CreateMedicAsync(MedicViewModel medicViewModel);
        Task<ApiResponse<MedicDto>> UpdateMedicAsync(MedicViewModel medicViewModel);
        Task<ApiResponse<MedicDto>> DeleteMedicAsync(Guid id);

    } 
}
