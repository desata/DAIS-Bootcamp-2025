using ExamPrep.Services.Interfaces;
using ExamPrep.Web.Attributes;
using ExamPrep.Web.Models;
using ExamPrep.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ExamPrep.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Home") });
            }

            var response = await _userService.GetAllUsersAsync();

            var viewModel = new UserListViewModel
            { 
                Users = response.Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Username = u.Username
                }).ToList(),     


            };

            return View(viewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
