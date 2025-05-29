using DAIS.WikiSystem.Services.Interfaces.Category;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.Tag;
using DAIS.WikiSystem.WebApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DAIS.WikiSystem.WebApp.Controllers
{
    public class DocumentController : Controller
    {
        private readonly IDocumentService _documentService;
        private readonly IDocumentVersionService _documentVersionService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public DocumentController(
            IDocumentService documentService,
            IDocumentVersionService documentVersionService,
            ICategoryService categoryService,
            ITagService tagService)
        {
            _documentService = documentService;
            _documentVersionService = documentVersionService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _documentService.GetAllAsync();

            var viewModel = new DocumentListViewModel
            {
                Documents = response.Documents.Select(d => new DocumentViewModel
                {
                    DocumentId = d.DocumentId,
                    CategoryName = d.CategoryName,
                    Title = d.Title,
                    CreatorFirstName = d.CreatorFirstName,
                    CreatorLastName = d.CreatorLastName,
                    AccessLevel = d.AccessLevel,
                    IsDeleted = d.IsDeleted,
                    Tags = d.Tags

                })
                .ToList(),
            };
            return View(viewModel);
        }
    }
}
