using Exam.Services.DTOs.Favorite;
using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Exam.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IWorkplaceService _workplaceService;
        private readonly IFavoriteService _favoriteService;

        public FavoritesController(IWorkplaceService workplaceService, IFavoriteService favoriteService)
        {
            _workplaceService = workplaceService;
            _favoriteService = favoriteService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
                return RedirectToAction("Login", "Account");

            var workplaces = await _workplaceService.GetAllWorkplacesAsync();
            var favorites = await _favoriteService.GetAllByUserIdAsync(userId.Value);

            var favoriteIds = favorites.Select(f => f.WorkplaceId).ToHashSet();

            var model = workplaces.Select(wp =>
            {
                var fav = favorites.FirstOrDefault(f => f.WorkplaceId == wp.WorkplaceId);
                return new WorkplaceFavoriteViewModel
                {
                    WorkplaceId = wp.WorkplaceId,
                    FavoriteId = fav?.FavoriteId,
                    Location = wp.Location,
                    HasMonitor = wp.HasMonitor,
                    HasDockingStation = wp.HasDockingStation,
                    HasWindow = wp.HasWindow,
                    HasPrinter = wp.HasPrinter
                };
            }).ToList();

            ViewBag.FavoriteCount = favoriteIds.Count;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddFavorite(int workplaceId, string name)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var favorites = await _favoriteService.GetAllByUserIdAsync(userId);
            if (favorites.Count >= 3)
            {
                TempData["Error"] = "Maximum of 3 favorites allowed.";
                return RedirectToAction("Index");
            }

            await _favoriteService.CreateFavoriteAsync(new CreateFavoriteRequest
            {
                UserId = userId,
                WorkplaceId = workplaceId,
                Name = name
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int favoriteId)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            await _favoriteService.DeleteFavoriteAsync(new DeleteFavoriteRequest
            {
                UserId = userId,
                FavoriteId = favoriteId
            });

            return RedirectToAction("Index");
        }

    }
}