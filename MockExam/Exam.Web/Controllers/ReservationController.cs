using Exam.Services.DTOs.Reservation;
using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var request = new DeleteReservationRequest
            {
                ReservationId = reservationId,
                UserId = userId
            };

            var result = await _reservationService.DeleteReservationAsync(request);

            if (!result.Success)
            {
                TempData["Error"] = result.ErrorMessage;
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
