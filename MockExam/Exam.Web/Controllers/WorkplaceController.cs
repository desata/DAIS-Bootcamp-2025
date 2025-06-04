using Exam.Services.DTOs.Reservation;
using Exam.Services.Implementation;
using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Exam.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    [Authorize]
    public class WorkplaceController : Controller
    {
        private readonly IWorkplaceService _workplaceService;
        private readonly IReservationService _reservationService;
        private readonly IFavoriteService _favoriteService;

        private readonly IUserService _userService;

        public WorkplaceController(ILogger<HomeController> logger, IUserService userService, IWorkplaceService workplaceService, IReservationService reservationService, IFavoriteService favoriteService)
        {
            _userService = userService;
            _workplaceService = workplaceService;
            _reservationService = reservationService;
            _favoriteService = favoriteService;
        }


        [HttpPost]
        public async Task<IActionResult> Reserve(int workplaceId)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Home") });
            }

            var request = new CreateReservationRequest
            {
                UserId = userId.Value,
                WorkplaceId = workplaceId,
                ReservationDate = DateTime.Today.AddDays(1)
            };

            try
            {
                var result = await _reservationService.CreateReservationAsync(request);
                TempData["SuccessMessage"] = "Reservation created successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ReserveFuture(int workplaceId, DateTime reservationDate)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Home") });
            }

            var request = new CreateReservationRequest
            {
                UserId = userId.Value,
                WorkplaceId = workplaceId,
                ReservationDate = reservationDate
            };

            var response = await _reservationService.CreateReservationAsync(request);

            if (!response.Success)
            {
                TempData["Error"] = response.ErrorMessage;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
