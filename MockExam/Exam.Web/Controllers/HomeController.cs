using Exam.Models;
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
        private readonly IReservationService _reservationService;
        private readonly IFavoriteService _favoriteService;

        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, IWorkplaceService workplaceService, IReservationService reservationService, IFavoriteService favoriteService)
        {
            _logger = logger;
            _userService = userService;
            _workplaceService = workplaceService;
            _reservationService = reservationService;
            _favoriteService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var workplaces = await _workplaceService.GetAvailableWorkplacesAsync();
            var favorites = await _favoriteService.GetAllByUserIdAsync(userId);

            var workplacesVModel = workplaces.Select(wp => new WorkplaceInfo
            {
                WorkplaceId = wp.WorkplaceId,
                HasMonitor = wp.HasMonitor,
                HasDockingStation = wp.HasDockingStation,
                HasWindow = wp.HasWindow,
                HasPrinter = wp.HasPrinter,
                IsAvailable = wp.IsAvailable,
                Location = wp.Location,
                IsFavorite = favorites.Any(f => f.WorkplaceId == wp.WorkplaceId)
            }).OrderByDescending(w => w.IsFavorite)
              .ThenBy(w => w.FavoriteId ?? int.MaxValue)
              .ToList();

            var reservations = await _reservationService.GetAllByUserIdAsync(userId);

            var viewModel = new HomeViewModel
            {
                AvailableWorkplaces = new WorkplaceListViewModel
                {
                    TotalCount = workplacesVModel.Count,
                    Workplaces = workplacesVModel
                },
                UserReservations = reservations
            };

            return View(viewModel);

        }
    }
}
