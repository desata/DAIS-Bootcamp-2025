using Exam.Services.Interfaces;
using Exam.Web.Attributes;
using Exam.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Exam.Web.Controllers
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
                    Email = u.Email,
                }).ToList()
            };

            return View(viewModel);
        }

    }
}
