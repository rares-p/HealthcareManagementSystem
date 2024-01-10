using HealthcareManagementSystem.App.Services.Responses;
using HealthcareManagementSystem.App.ViewModels;

namespace HealthcareManagementSystem.App.Contracts
{
	public interface IExaminationDataService
	{
        Task<List<ExaminationViewModel>> GetExaminationsAsync();
        Task<ApiResponse<ExaminationViewModel>> CreateExaminationAsync(CreateExaminationViewModel examinationViewModel);


    }
}
