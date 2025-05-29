using Microsoft.AspNetCore.Mvc;

namespace BirthdayGiftApp.Controllers
{
    public class VoteResultController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VoteResultController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VoteResult/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var vote = await _context.GiftVotes
                .Include(v => v.GiftVoteOptions)
                .ThenInclude(go => go.Gift)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsActive);

            if (vote == null)
            {
                return View("Error");
            }

            var model = new VoteResultViewModel
            {
                VoteId = id,
                VoteOptions = vote.GiftVoteOptions.Select(go => go.Gift).ToList(),
                VoteSelections = await _context.GiftVoteSelections
                    .Include(vs => vs.Gift)
                    .Include(vs => vs.VoterEmployee)
                    .Where(vs => vs.GiftVoteId == id)
                    .ToListAsync()
            };

            return View(model);
        }
    }

}
