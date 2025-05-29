using DAIS.WikiSystem.Services.Interfaces.Category;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.Tag;
using DAIS.WikiSystem.WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DAIS.WikiSystem.WebApp.Controllers
{
    public class DocumentVersionController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IDocumentVersionService _documentVersionService;

        public DocumentVersionController(
            IDocumentService documentService,
            IDocumentVersionService documentVersionService)
        {
            _documentService = documentService;
            _documentVersionService = documentVersionService;
        }

        public async Task<IActionResult> Index()
        {
            return View();

        }
    }
}
