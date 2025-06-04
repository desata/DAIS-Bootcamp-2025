using Exam.Services.DTOs.Workplace;

namespace Exam.Services.Interfaces
{
    public interface IWorkplaceService
    {
        Task<List<WorkplaceInfo>> GetAllWorkplacesAsync();
        Task<List<WorkplaceInfo>> GetAvailableWorkplacesAsync();
        Task<WorkplaceInfo?> GetByWorkplaceIdAsync(int workpaseId);
        Task<UpdateWorkplaceAvailabilityResponse> UpdateWorkPlaceAvailability(UpdateWorkplaceAvailabilityRequest request);
    }
}
