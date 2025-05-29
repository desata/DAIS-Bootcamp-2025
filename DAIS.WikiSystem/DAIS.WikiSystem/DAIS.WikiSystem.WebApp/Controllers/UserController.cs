using Microsoft.AspNetCore.Mvc;

namespace DAIS.WikiSystem.WebApp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
