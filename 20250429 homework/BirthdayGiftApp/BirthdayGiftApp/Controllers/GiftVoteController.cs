using BirthdayGiftApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BirthdayGiftApp.Controllers
{
    public class GiftVoteController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Employee> _userManager;

        public GiftVoteController(ApplicationDbContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: GiftVote/StartVote
        public async Task<IActionResult> StartVote()
        {
            var employees = await _context.Employees.ToListAsync();
            var gifts = await _context.Gifts.ToListAsync();

            var model = new StartVoteViewModel
            {
                Employees = employees,
                Gifts = gifts
            };

            return View(model);
        }

        // POST: GiftVote/StartVote
        [HttpPost]
        public async Task<IActionResult> StartVote(StartVoteViewModel model)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var birthdayEmployee = await _context.Employees.FindAsync(model.BirthdayEmployeeId);

                if (birthdayEmployee == null || currentUser.Id == birthdayEmployee.UserId)
                {
                    // Prevent a user from voting for themselves or choosing invalid employee
                    return View("Error");
                }

                // Check if an active vote already exists for this employee's birthday
                bool existingVote = await _context.GiftVotes
                    .AnyAsync(v => v.BirthdayEmployeeId == model.BirthdayEmployeeId && v.IsActive);

                if (existingVote)
                {
                    return View("Error");
                }

                var newVote = new GiftVote
                {
                    BirthdayEmployeeId = model.BirthdayEmployeeId,
                    StartedByEmployeeId = currentUser.Id,
                    BirthdayDate = birthdayEmployee.DateOfBirth, // Assuming BirthdayDate for the year
                    IsActive = true
                };

                _context.GiftVotes.Add(newVote);
                await _context.SaveChangesAsync();

                foreach (var giftId in model.SelectedGifts)
                {
                    _context.GiftVoteOptions.Add(new GiftVoteOption
                    {
                        GiftVoteId = newVote.Id,
                        GiftId = giftId
                    });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Dashboard", "Home");
            }

            return View(model);
        }

        // GET: GiftVote/Vote/5
        public async Task<IActionResult> Vote(int id)
        {
            var vote = await _context.GiftVotes
                .Include(v => v.GiftVoteOptions)
                .ThenInclude(go => go.Gift)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vote == null || !vote.IsActive)
            {
                return View("Error"); // No active vote found
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser.Id == vote.BirthdayEmployeeId)
            {
                return View("Error");
            }

            var model = new VoteViewModel
            {
                VoteId = id,
                GiftVoteOptions = vote.GiftVoteOptions.Select(go => go.Gift).ToList()
            };

            return View(model);
        }

        // POST: GiftVote/Vote/5
        [HttpPost]
        public async Task<IActionResult> Vote(int id, VoteViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            // Check if the user has already voted
            var existingVote = await _context.GiftVoteSelections
                .FirstOrDefaultAsync(v => v.GiftVoteId == id && v.VoterEmployeeId == currentUser.Id);

            if (existingVote != null)
            {
                return View("Error");
            }

            // Record the user's vote
            var voteSelection = new GiftVoteSelection
            {
                GiftVoteId = id,
                VoterEmployeeId = currentUser.Id,
                GiftId = model.SelectedGiftId
            };

            _context.GiftVoteSelections.Add(voteSelection);
            await _context.SaveChangesAsync();

            return RedirectToAction("VoteResult", new { id = id });
        }

        // GET: GiftVote/EndVote/5
        public async Task<IActionResult> EndVote(int id)
        {
            var vote = await _context.GiftVotes
                .FirstOrDefaultAsync(v => v.Id == id);

            if (vote == null || vote.StartedByEmployeeId != User.Identity.Name)
            {
                return View("Error"); // Only the creator can end the vote
            }

            vote.IsActive = false;
            _context.GiftVotes.Update(vote);
            await _context.SaveChangesAsync();

            return RedirectToAction("VoteResult", new { id = id });
        }
    }

}
