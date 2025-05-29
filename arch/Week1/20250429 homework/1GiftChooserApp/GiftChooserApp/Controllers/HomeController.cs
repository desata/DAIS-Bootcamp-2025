using System.Diagnostics;
using GiftChooserApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GiftChooserApp.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
           
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
