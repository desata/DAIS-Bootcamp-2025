using Exam.Models;
using Exam.Repository.Interfaces;
using Exam.Services.DTOs.Workplace;
using Exam.Services.Interfaces;

namespace Exam.Services.Implementation
{
    public class WorkplaceService : IWorkplaceService
    {
        private readonly IWorkplaceRepository _workplaceRepository;

        public WorkplaceService(IWorkplaceRepository workplaceRepository)
        {
            _workplaceRepository = workplaceRepository;
        }

        public async Task<List<WorkplaceInfo>> GetAllWorkplacesAsync()
        {
            var workplaces = await _workplaceRepository.RetrieveCollectionAsync();
            return workplaces.Select(workplace => new WorkplaceInfo
            {
                WorkplaceId = workplace.WorkplaceId,
                HasMonitor = workplace.HasMonitor,
                HasDockingStation = workplace.HasDockingStation,
                HasWindow = workplace.HasWindow,
                HasPrinter = workplace.HasPrinter,
                Location = workplace.Location,
                IsAvailable = workplace.IsAvailable
            }).ToList();
        }


        public async Task<List<WorkplaceInfo>> GetAvailableWorkplacesAsync()
        {
            var workplaces = await _workplaceRepository.RetrieveCollectionAsync();
            return workplaces.Where(w => w.IsAvailable).Select(workplace => new WorkplaceInfo
            {
                WorkplaceId = workplace.WorkplaceId,
                HasMonitor = workplace.HasMonitor,
                HasDockingStation = workplace.HasDockingStation,
                HasWindow = workplace.HasWindow,
                HasPrinter = workplace.HasPrinter,
                Location = workplace.Location,
                IsAvailable = workplace.IsAvailable
            }).ToList();
        }

        public async Task<WorkplaceInfo?> GetByWorkplaceIdAsync(int workpaseId)
        {
            var workplace = await _workplaceRepository.RetrieveByIdAsync(workpaseId);

            return new WorkplaceInfo
            {
                WorkplaceId = workplace.WorkplaceId,
                HasMonitor = workplace.HasMonitor,
                HasDockingStation = workplace.HasDockingStation,
                HasWindow = workplace.HasWindow,
                HasPrinter = workplace.HasPrinter,
                Location = workplace.Location,
                IsAvailable = workplace.IsAvailable
            };
        }

        public async Task<UpdateWorkplaceAvailabilityResponse> UpdateWorkPlaceAvailability(UpdateWorkplaceAvailabilityRequest request)
        {
            try
            {
                var workplace = new Workplace
                {
                    WorkplaceId = request.WorkplaceId,
                    IsAvailable = request.IsAvailable
                };

                var success = await _workplaceRepository.UpdateAsync(workplace);

                var response = new UpdateWorkplaceAvailabilityResponse();

                if (success)
                {
                    response.Success = true;
                }
                else
                {
                    response.Success = false;
                    response.ErrorMessage = "Workplace not found or update failed.";
                }

                return response;
            }
            catch (Exception ex)
            {
                return new UpdateWorkplaceAvailabilityResponse
                {
                    Success = false,
                    ErrorMessage = $"An error occurred while updating the workplace: {ex.Message}"
                };
            }
        }
    }
}