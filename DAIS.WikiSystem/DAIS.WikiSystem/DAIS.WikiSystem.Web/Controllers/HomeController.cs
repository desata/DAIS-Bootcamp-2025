using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository.Interfaces.User;
using DAIS.WikiSystem.Services.DTOs.Document;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Web.Attributes;
using DAIS.WikiSystem.Web.Models;
using DAIS.WikiSystem.Web.Models.ViewModels.Document;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace DAIS.WikiSystem.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IDocumentService documentService, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var accessLevelInt = HttpContext.Session.GetInt32("AccessLevel");

            if (!userId.HasValue || !accessLevelInt.HasValue)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index", "Home") });
            }

            var accessLevel = (AccessLevel)accessLevelInt.Value;

            var response = await _documentService.GetAllByCreatorIdAsync(userId.Value);


            var viewModel = new DocumentListViewModel
            {
                Documents = response.Documents.Select(d => new DocumentViewModel
                {
                    DocumentId = d.DocumentId,
                    Title = d.Title,
                    CategoryName = d.CategoryName,
                    CreatorFirstName = d.CreatorFirstName,
                    CreatorLastName = d.CreatorLastName,
                    AccessLevel = d.AccessLevel,
                    IsDeleted = d.IsDeleted,
                    Tags = d.Tags
                }).ToList()
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
