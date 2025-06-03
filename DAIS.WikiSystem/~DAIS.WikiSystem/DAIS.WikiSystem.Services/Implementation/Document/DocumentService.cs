using DAIS.WikiSystem.Models;
using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository.Interfaces.Document;
using DAIS.WikiSystem.Repository.Interfaces.Mappers;
using DAIS.WikiSystem.Repository.Interfaces.Tag;
using DAIS.WikiSystem.Repository.Interfaces.User;
using DAIS.WikiSystem.Services.DTOs.Document;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Services.Interfaces.User;
using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Services.Implementation.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserService _userContext;
        private readonly IDocumentTagRepository _docTagRepo;
        private readonly ITagRepository _tagRepo;

        public DocumentService(IDocumentRepository documentRepository, IUserService userContext, IDocumentTagRepository docTagRepo, ITagRepository tagRepo)
        {
            _documentRepository = documentRepository;
            _userContext = userContext;
            _docTagRepo = docTagRepo;
            _tagRepo = tagRepo;
        }

        public async Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request)
        {

            var existingDocuments = await _documentRepository
                 .RetrieveCollectionAsync(new DocumentFilter
                 {
                     Title = new SqlString(request.Title),
                     CreatorId = request.CreatorId
                 })
                 .ToListAsync();

            if (existingDocuments.Any())
            {
                return new CreateDocumentResponse
                {
                    Success = false,
                    ErrorMessage = "You already have a document with this title."
                };
            }

            var document = new Models.Document
            {
                Title = request.Title,
                CreatorId = request.CreatorId,
                CategoryId = request.CategoryId,
                AccessLevel = request.AccessLevel,
                IsDeleted = request.IsDeleted
            };

            int newId = await _documentRepository.CreateAsync(document);

            if (request.TagIds?.Any() == true)
            {
                foreach (var tagId in request.TagIds)
                {
                    int effectiveTagId = tagId;

                    // Verify tag exists
                    var tag = await _tagRepo.RetrieveAsync(tagId);
                    if (tag == null)
                    {
                        // Create new tag and use its returned ID
                        effectiveTagId = await _tagRepo.CreateAsync(new Models.Tag { Name = $"Tag{tagId}" });
                    }

                    // Map document ↔ tag
                    await _docTagRepo.CreateMappingIfNotExistsAsync(
                        new DocumentTag { DocumentId = newId, TagId = effectiveTagId }
                    );
                }
            }

            return new CreateDocumentResponse
            {
                Success = true,
                DocumentId = newId
            };

        }

        public async Task<GetAllDocumentsResponse> GetByAccessLevelAsync(int accessLevelId)
        {
            var documents = await _documentRepository
               .RetrieveCollectionAsync(new DocumentFilter { AccessLevel = (AccessLevel)accessLevelId })
               .ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = documents.Select(MapToDocumentInfo).ToList(),
                TotalCount = documents.Count
            };

            return response;
        }


        public async Task<GetAllDocumentsResponse> GetByCategoryIdAsync(int categoryId)
        {
            var documents = await _documentRepository
               .RetrieveCollectionAsync(new DocumentFilter { CategoryId = categoryId })
               .ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = documents.Select(MapToDocumentInfo).ToList(),
                TotalCount = documents.Count
            };

            return response;
        }

        public async Task<GetAllDocumentsResponse> GetByCreatorIdAsync(int creatorId)
        {
            var documents = await _documentRepository
                .RetrieveCollectionAsync(new DocumentFilter { CreatorId = creatorId })
                .ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = documents.Select(MapToDocumentInfo).ToList(),
                TotalCount = documents.Count
            };

            return response;

        }

        public async Task<GetDocumentResponse> GetByIdAsync(int documentId)
        {
            var document = await _documentRepository.RetrieveAsync(documentId);

            return (GetDocumentResponse)MapToDocumentInfo(document);

        }

        public async Task<GetAllDocumentsResponse> GetByTitleAsync(string title)
        {
            var documents = await _documentRepository
                .RetrieveCollectionAsync(new DocumentFilter { Title = title })
                .ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = documents.Select(MapToDocumentInfo).ToList(),
                TotalCount = documents.Count
            };

            return response;
        }


        private DocumentInfo MapToDocumentInfo(Models.Document document)
        {
            return new DocumentInfo
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                AccessLevel = (AccessLevel)document.AccessLevel,
                IsDeleted = document.IsDeleted,
                CreatorId = document.CreatorId,
                CategoryId = document.CategoryId
            };

        }
    }
}
