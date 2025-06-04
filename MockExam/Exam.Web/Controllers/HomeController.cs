using Exam.Services.Implementation;
using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Exam.Web.Models;
using Exam.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Exam.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWorkplaceService _workplaceService;

        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IWorkplaceService workplaceService)
        {
            _logger = logger;
            _userService = userService;
            _workplaceService = workplaceService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Home") });
            }

            var response = await _userService.GetAllUsersAsync();

            var viewModel = new UserListViewModel
            { 
                Users = response.Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Name = u.Name
                }).ToList(),     


            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
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
