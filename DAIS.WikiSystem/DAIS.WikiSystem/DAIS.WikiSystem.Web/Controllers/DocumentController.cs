using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Services.Interfaces.Category;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.Tag;
using DAIS.WikiSystem.Web.Attributes;
using DAIS.WikiSystem.Web.Models.ViewModels.Document;
using Microsoft.AspNetCore.Mvc;

namespace DAIS.WikiSystem.Web.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IDocumentVersionService _documentVersionService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public DocumentController(
            IDocumentService documentService, ICategoryService categoryService, ITagService tagService)
        {
            _documentService = documentService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var accessLevel = (AccessLevel)HttpContext.Session.GetInt32("AccessLevel").Value;

            var response = await _documentService.GetAllActiveAsync(accessLevel);

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

        public async Task<IActionResult> Info(int id)
        {
            var documentInfo = await _documentService.GetByIdAsync(id);


            var viewModel = new DocumentViewModel
            {
                Title = documentInfo.Title,
                AccessLevel = documentInfo.AccessLevel,
                CreatorFirstName = documentInfo.CreatorFirstName,
                CreatorLastName = documentInfo.CreatorLastName,
                CategoryName = documentInfo.CategoryName,
                Tags = documentInfo.Tags,
                Version = documentInfo.Version,
                FilePath = documentInfo.FilePath,
                CreateDate = documentInfo.CreateDate
            };

            return View(viewModel);
        }
    }
}
