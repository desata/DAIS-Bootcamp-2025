using Exam.Services.DTOs.Reservation;
using Exam.Services.DTOs.Workplace;
using Exam.Services.Implementation;
using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
{
    [Authorize]
    public class WorkplaceController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IWorkplaceService _workplaceService;

        public WorkplaceController(IReservationService reservationService, IWorkplaceService workplaceService)
        {

            _reservationService = reservationService;
            _workplaceService = workplaceService;
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

            var result = await _reservationService.CreateReservationAsync(request);

            if (result.Success)
            {
                // Mark workplace as unavailable after successful reservation
                var updateResponse = await _workplaceService.UpdateWorkPlaceAvailability(new UpdateWorkplaceAvailabilityRequest
                {
                    WorkplaceId = workplaceId,
                    IsAvailable = false
                });

                if (!updateResponse.Success)
                {
                    TempData["Error"] = updateResponse.ErrorMessage;
                }
                else
                {
                    TempData["SuccessMessage"] = "Reservation created successfully.";
                }
            }
            else
            {
                TempData["Error"] = result.ErrorMessage;
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

            var result = await _reservationService.CreateReservationAsync(request);

            if (result.Success)
            {
                // Mark workplace as unavailable after successful reservation
                var updateResponse = await _workplaceService.UpdateWorkPlaceAvailability(new UpdateWorkplaceAvailabilityRequest
                {
                    WorkplaceId = workplaceId,
                    IsAvailable = false
                });

                if (!updateResponse.Success)
                {
                    TempData["Error"] = updateResponse.ErrorMessage;
                }
                else
                {
                    TempData["SuccessMessage"] = "Reservation created successfully.";
                }
            }
            else
            {
                TempData["Error"] = result.ErrorMessage;
            }

            return RedirectToAction("Index", "Home");
        }
    }
}