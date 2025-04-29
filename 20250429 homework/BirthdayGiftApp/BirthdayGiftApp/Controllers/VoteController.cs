using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BirthdayGiftApp.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        private readonly IVoteService _voteService;
        private readonly UserManager<Employee> _userManager;

        public VoteController(IVoteService voteService, UserManager<Employee> userManager)
        {
            _voteService = voteService;
            _userManager = userManager;
        }

        public IActionResult Start() => View();

        [HttpPost]
        public async Task<IActionResult> Start(string targetEmployeeId)
        {
            var currentUserId = _userManager.GetUserId(User);
            var success = await _voteService.StartVoteAsync(currentUserId, targetEmployeeId);
            return success ? RedirectToAction("Index") : BadRequest("Vote already exists or invalid.");
        }

        [HttpPost]
        public async Task<IActionResult> Vote(int voteOptionId)
        {
            var currentUserId = _userManager.GetUserId(User);
            var success = await _voteService.VoteAsync(currentUserId, voteOptionId);
            return success ? RedirectToAction("Index") : BadRequest("Already voted or invalid option.");
        }

        [HttpPost]
        public async Task<IActionResult> End(int voteId)
        {
            var currentUserId = _userManager.GetUserId(User);
            var success = await _voteService.EndVoteAsync(voteId, currentUserId);
            return success ? RedirectToAction("Results", new { voteId }) : BadRequest("Not allowed.");
        }

        public async Task<IActionResult> Results(int voteId)
        {
            var vote = await _voteService.GetVoteWithResultsAsync(voteId);
            return View(vote);
        }
    }

}
