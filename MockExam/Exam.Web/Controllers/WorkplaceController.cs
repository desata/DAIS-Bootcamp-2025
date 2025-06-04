using Exam.Services.DTOs.Reservation;
using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    [Authorize]
    public class WorkplaceController : Controller
    {
        private readonly IReservationService _reservationService;

        public WorkplaceController(IReservationService reservationService)
        {

            _reservationService = reservationService;
        }


        [HttpPost]
        public async Task<IActionResult> Reserve(int workplaceId)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new CreateReservationRequest
            {
                UserId = userId,
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
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new CreateReservationRequest
            {
                UserId = userId,
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
