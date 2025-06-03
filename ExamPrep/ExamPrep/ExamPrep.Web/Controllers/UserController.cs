using ExamPrep.Services.Interfaces;
using ExamPrep.Web.Attributes;
using ExamPrep.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExamPrep.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            var users = await _userService.GetAllUsersAsync();

            var viewModel = new UserListViewModel
            {
                TotalCount = users.Count,
                Users = users.Select(u => new UserViewModel
                {
                    UserId = u.UserId,
                    Name = $"{u.Name}",
                    Username = u.Username
                }).ToList()
            };

            return View(viewModel);
        }

    }
}