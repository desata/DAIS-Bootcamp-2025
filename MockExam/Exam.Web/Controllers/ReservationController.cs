using Exam.Services.DTOs.Reservation;
using Exam.Services.DTOs.Workplace;
using Exam.Services.Implementation;
using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IWorkplaceService _workplaceService;

        public ReservationController(IReservationService reservationService, IWorkplaceService workplaceService)
        {
            _reservationService = reservationService;
            _workplaceService = workplaceService;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int reservationId)
        {
            int userId = HttpContext.Session.GetInt32("UserId").Value;

            if (!HttpContext.Session.GetInt32("UserId").HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            // First, get the reservation to find out the workplaceId
            var reservation = await _reservationService.GetByIdAsync(reservationId);
            if (reservation == null || reservation.UserId != userId)
            {
                TempData["Error"] = "Reservation not found or access denied.";
                return RedirectToAction("Index", "Home");
            }

            // Delete the reservation
            var deleteRequest = new DeleteReservationRequest
            {
                ReservationId = reservationId,
                UserId = userId
            };
            var deleteResult = await _reservationService.DeleteReservationAsync(deleteRequest);

            if (!deleteResult.Success)
            {
                TempData["Error"] = deleteResult.ErrorMessage;
                return RedirectToAction("Index", "Home");
            }

            // Mark the workplace as available again
            var updateResponse = await _workplaceService.UpdateWorkPlaceAvailability(new UpdateWorkplaceAvailabilityRequest
            {
                WorkplaceId = reservation.WorkplaceId,
                IsAvailable = true
            });

            if (!updateResponse.Success)
            {
                TempData["Error"] = updateResponse.ErrorMessage;
            }

            return RedirectToAction("Index", "Home");
        }

    }
}
