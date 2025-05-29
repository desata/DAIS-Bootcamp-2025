using DAIS.WikiSystem.Models;
using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository.Interfaces;
using DAIS.WikiSystem.Repository.Interfaces.Document;
using DAIS.WikiSystem.Repository.Interfaces.DocumentTag;
using DAIS.WikiSystem.Repository.Interfaces.Tag;
using DAIS.WikiSystem.Repository.Interfaces.User;
using DAIS.WikiSystem.Services.DTOs.Document;
using DAIS.WikiSystem.Services.Interfaces.Document;
using System.Data.SqlTypes;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace DAIS.WikiSystem.Services.Implementation.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDocumentTagRepository _documentTagRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;

        public DocumentService(IDocumentRepository documentRepository, IUserRepository userRepository, IDocumentTagRepository docTagRepo, ITagRepository tagRepo, ICategoryRepository categoryRepository)
        {
            _documentRepository = documentRepository;
            _userRepository = userRepository;
            _documentTagRepository = docTagRepo;
            _tagRepository = tagRepo;
            _categoryRepository = categoryRepository;
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

                    var tag = await _tagRepository.RetrieveAsync(tagId);
                    if (tag == null)
                    {

                        effectiveTagId = await _tagRepository.CreateAsync(new Models.Tag { Name = $"Tag{tagId}" });
                    }

                    await _documentTagRepository.CreateMappingIfNotExistsAsync(
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
            var filter = new DocumentFilter
            {
                AccessLevel = (AccessLevel)accessLevelId,
                IsDeleted = false
            };
            var documents = await _documentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = new List<DocumentInfo>(),
                TotalCount = documents.Count
            };

            foreach (var document in documents)
            {
                var documentInfo = await MapToDocumentFullInfo(document);
                response.Documents.Add(documentInfo);
            }

            return response;
        }

        public async Task<GetAllDocumentsResponse> GetByCategoryIdAsync(int categoryId)
        {
            var filter = new DocumentFilter
            {
                IsDeleted = false,
                CategoryId = categoryId
            };

            var documents = await _documentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = new List<DocumentInfo>(),
                TotalCount = documents.Count
            };

            foreach (var document in documents)
            {
                var documentInfo = await MapToDocumentFullInfo(document);
                response.Documents.Add(documentInfo);
            }

            return response;
        }

        public async Task<GetAllDocumentsResponse> GetByCreatorIdAsync(int creatorId)
        {
            var filter = new DocumentFilter
            {
                IsDeleted = false,
                CreatorId = creatorId
            };

            var documents = await _documentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = new List<DocumentInfo>(),
                TotalCount = documents.Count
            };

            foreach (var document in documents)
            {
                var documentInfo = await MapToDocumentFullInfo(document);
                response.Documents.Add(documentInfo);
            }

            return response;
        }


        public async Task<GetAllDocumentsResponse> GetAllAsync()
        {
            var filter = new DocumentFilter
            {
                IsDeleted = false
            };

            var documents = await _documentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = new List<DocumentInfo>(),
                TotalCount = documents.Count
            };

            foreach (var document in documents)
            {
                var documentInfo = await MapToDocumentFullInfo(document);
                response.Documents.Add(documentInfo);
            }

            return response;
        }


        public async Task<GetAllDocumentsResponse> GetByTitleAsync(string title)
        {
            var filter = new DocumentFilter
            {
                Title = title,
                IsDeleted = false
            };

            var documents = await _documentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var response = new GetAllDocumentsResponse
            {
                Documents = new List<DocumentInfo>(),
                TotalCount = documents.Count
            };

            foreach (var document in documents)
            {
                var documentInfo = await MapToDocumentFullInfo(document);
                response.Documents.Add(documentInfo);
            }

            return response;
        }

        private async Task<DocumentInfo> MapToDocumentFullInfo(Models.Document document)
        {

            var creator = await _userRepository.RetrieveAsync(document.CreatorId);
            var category = await _categoryRepository.RetrieveAsync(document.CategoryId);
            var tagMappingIds = await _documentTagRepository.RetrieveCollectionAsync(new DocumentTagFilter { DocumentId = document.DocumentId }).Select(tm => tm.TagId).ToListAsync();
            var tags = new List<Models.Tag>();

            if (tagMappingIds.Any())
            {
                foreach (var item in tagMappingIds)
                {
                    tags.Add(await _tagRepository.RetrieveAsync(item));
                }               
            }

            return new DocumentInfo
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                AccessLevel = (AccessLevel)document.AccessLevel,
                IsDeleted = document.IsDeleted,
                CreatorId = document.CreatorId,
                CreatorFirstName = creator.FirstName,
                CreatorLastName = creator.LastName,
                CategoryName = category.Name,
                Tags = tags.Select(t => t.Name).ToList(),
            };
        }

        public async Task<GetDocumentResponse> GetByIdAsync(int documentId)
        {
            var filter = new DocumentFilter
            {
                DocumentId = documentId,
                IsDeleted = false
            };

            var document = await _documentRepository.RetrieveCollectionAsync(filter).FirstOrDefaultAsync();

            var response = (GetDocumentResponse)await MapToDocumentFullInfo(document);
            return response;
        }

    }
}
