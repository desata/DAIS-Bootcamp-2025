using DAIS.WikiSystem.Services.Interfaces.Collection;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Web.Attributes;
using DAIS.WikiSystem.Web.Models.ViewModels.Collection;
using Microsoft.AspNetCore.Mvc;

namespace DAIS.WikiSystem.Web.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly ICollectionService _collectionService;
        private readonly IDocumentService _documentService;

        public CollectionController(ICollectionService collectionService, IDocumentService documentService)
        {
            _collectionService = collectionService;
            _documentService = documentService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
               
            
            var response = await _collectionService.GetAllByCreatorAsync(userId);

            var viewModel = new CollectionsListViewModel
            {
                Collections = response.Collections.Select(d => new CollectionViewModel
                {
                    CollectionId = d.CollectionId,
                    Name = d.Name,
                    CreatorId = d.CreatorId,
                    CreateDate = d.CreateDate

                }).ToList()
            };

            return View(viewModel);
            }
        
    }
}