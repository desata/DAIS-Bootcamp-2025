using Exam.Services.Interfaces;
using Exam.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    public class WorkplaceController : Controller
    {

        private readonly IWorkplaceService _workplaceService;
        public WorkplaceController(IWorkplaceService workplaceService)
        {
            _workplaceService = workplaceService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Free(bool filterHasMonitor, bool filterHasDockingStation, bool filterHasWindow, bool filterHasPrinter)
        {
            var dtoList = await _workplaceService.GetAvailableWorkplacesAsync();

            var viewModel = new WorkplaceListViewModel
            {
                Workplaces = dtoList.Select(dto => new WorkplaceInfo
                {
                    WorkplaceId = dto.WorkplaceId,
                    HasMonitor = dto.HasMonitor,
                    HasDockingStation = dto.HasDockingStation,
                    HasWindow = dto.HasWindow,
                    HasPrinter = dto.HasPrinter,
                    IsAvailable = dto.IsAvailable,
                    Location = dto.Location
                }).ToList(),

                TotalCount = dtoList.Count,

                FilterHasMonitor = filterHasMonitor,
                FilterHasDockingStation = filterHasDockingStation,
                FilterHasWindow = filterHasWindow,
                FilterHasPrinter = filterHasPrinter
            };
            return View(viewModel);
        }
    }
}
