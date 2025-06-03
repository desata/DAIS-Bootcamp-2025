using DAIS.WikiSystem.Services.DTOs.Authentication;
using DAIS.WikiSystem.Services.Interfaces.Authentication;
using DAIS.WikiSystem.Web.Models.ViewModels.Account;
using Microsoft.AspNetCore.Mvc;

namespace DAIS.WikiSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticationService _authService;

        public AccountController(IAuthenticationService authService)
        {
            _authService = authService;
        }



        [HttpGet]
        public IActionResult Login(string returnUrl = "/")
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.LoginAsync(new LoginRequest
            {
                Username = model.Username,
                Password = model.Password
            });

            if (result.IsSuccess)
            {
                HttpContext.Session.SetInt32("UserId", result.UserId.Value);
                HttpContext.Session.SetString("FirstName", result.FirstName);
                HttpContext.Session.SetString("LastName", result.LastName);
                HttpContext.Session.SetInt32("AccessLevel", (int)result.AccessLevel.Value);
                HttpContext.Session.SetInt32("Role", (int)result.Role.Value);


                if (!string.IsNullOrEmpty(model.ReturnUrl))
                    return Redirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ErrorMessage"] = result.ErrorMessage ?? "Invalid username or password";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
