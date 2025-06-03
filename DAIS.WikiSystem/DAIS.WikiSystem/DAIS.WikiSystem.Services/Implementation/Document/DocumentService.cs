using DAIS.WikiSystem.Models;
using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Repository.Interfaces.Category;
using DAIS.WikiSystem.Repository.Interfaces.Document;
using DAIS.WikiSystem.Repository.Interfaces.DocumentTag;
using DAIS.WikiSystem.Repository.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Repository.Interfaces.Tag;
using DAIS.WikiSystem.Repository.Interfaces.User;
using DAIS.WikiSystem.Services.DTOs.Document;
using DAIS.WikiSystem.Services.DTOs.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.Document;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;

namespace DAIS.WikiSystem.Services.Implementation.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly IDocumentVersionRepository _documentVersionRepository;
        private readonly IDocumentTagRepository _documentsTagsRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDocumentVersionService _documentVersionService;

        public DocumentService(IDocumentRepository documentRepository, 
            IDocumentVersionRepository documentVersionRepository, 
            IDocumentTagRepository documentTagRepository, 
            ITagRepository tagRepository,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository,
            IDocumentVersionService documentVersionService
            )
        {
            _documentRepository = documentRepository;
            _documentVersionRepository = documentVersionRepository;
            _documentsTagsRepository = documentTagRepository;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _documentVersionService = documentVersionService;
        }

        public async Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request)
        {
            var document = new Models.Document
            {
                Title = request.Title,
                CategoryId = request.CategoryId,
                CreatorId = request.CreatorId,
                AccessLevel = request.AccessLevel,
                IsDeleted = false
            };

            var documentId = await _documentRepository.CreateAsync(document);

            var versionResponse = await _documentVersionService.CreateDocumentVersionAsync(new CreateDocumentVersionRequest
            {
                DocumentId = documentId,
                FilePath = request.FilePath
            });

            if (request.TagIds is { Count: > 0 })
            {
                foreach (var tagId in request.TagIds)
                {
                    await _documentsTagsRepository.CreateMappingIfNotExistsAsync(new DocumentTag
                    {
                        DocumentId = documentId,
                        TagId = tagId
                    });
                }
            }

            return new CreateDocumentResponse
            {
                DocumentId = documentId,
                DocumentVersionId = versionResponse.DocumentVersionId
            };
        }

        public async Task<GetAllDocumentsResponse> GetAllActiveAsync(AccessLevel currentUserAccessLevel)
        {
            var filter = new DocumentFilter
            {
                IsDeleted = false,
                MaxAccessLevel = currentUserAccessLevel
            };
            var documents = await _documentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var mappedDocumentsTasks = documents.Select(MapToDocumentInfoAsync);
            var mappedDocuments = await Task.WhenAll(mappedDocumentsTasks);

            return new GetAllDocumentsResponse
            {
                Documents = mappedDocuments.ToList(),
                Count = mappedDocuments.Length
            };
        }

        public async Task<GetAllDocumentsResponse> GetAllAsync()
        {
            var documents = await _documentRepository.RetrieveCollectionAsync(new DocumentFilter()).ToListAsync();

            var mappedDocuments = await Task.WhenAll(documents.Select(MapToDocumentInfoAsync));

            return new GetAllDocumentsResponse
            {
                Documents = mappedDocuments.ToList(),
                Count = mappedDocuments.Length
            };
        }

        public async Task<GetAllDocumentsResponse> GetAllByCreatorIdAsync(int creatorId)
        {
            var filter = new DocumentFilter
            {
                CreatorId = creatorId
            };

            var documents = await _documentRepository.RetrieveCollectionAsync(filter).ToListAsync();

            var mappedDocuments = await Task.WhenAll(documents.Select(MapToDocumentInfoAsync));

            return new GetAllDocumentsResponse
            {
                Documents = mappedDocuments.ToList(),
                Count = mappedDocuments.Length
            };
        }

        public async Task<GetDocumentResponse> GetByIdAsync(int documentId)
        {            
            var document = await _documentRepository.RetrieveAsync(documentId);

            if (document == null)
            {
                throw new Exception("Document not found");
            }

            var creator = await _userRepository.RetrieveAsync(document.CreatorId);

            var category = await _categoryRepository.RetrieveAsync(document.CategoryId);
            
            var version = await _documentVersionRepository.RetrieveCollectionAsync(
                new DocumentVersionFilter { DocumentId = documentId , IsArchived = false}).FirstOrDefaultAsync();
            var documentTags = await _documentsTagsRepository.RetrieveCollectionAsync(
                new DocumentTagFilter { DocumentId = documentId }).ToListAsync();
            var tagNames = new List<string>();
            foreach (var dt in documentTags)
            {
                var tag = await _tagRepository.RetrieveAsync(dt.TagId);
                if (tag != null)
                {
                    tagNames.Add(tag.Name);
                }
            }

            var response = new GetDocumentResponse
            {
                Title = document.Title,
                AccessLevel = document.AccessLevel,
                Tags = tagNames,
                CreatorFirstName = creator?.FirstName,
                CreatorLastName = creator?.LastName,
                CategoryName = category?.Name,
                Version = version?.Version,
                FilePath = version?.FilePath,
                CreateDate = (DateTime)(version?.CreateDate)

            };
            return response;
        }

        public async Task<UpdateDocumentStateResponse> UpdateDocumentAsync(UpdateDocumentStateRequest request)
        {
            try
            {
                var document = await _documentRepository.RetrieveAsync(request.DocumentId);

                if (document == null)
                {
                    return new UpdateDocumentStateResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Document not found."
                    };
                }

                if (document.IsDeleted == request.IsDeletedNewStatus)
                {
                    return new UpdateDocumentStateResponse
                    {
                        IsSuccess = false,
                        ErrorMessage = "Document is already in the requested state."
                    };
                }

                document.IsDeleted = request.IsDeletedNewStatus;
            }
            catch (Exception ex)
            {
                return new UpdateDocumentStateResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"An error occurred while retrieving the document: {ex.Message}"
                };
            }

            return new UpdateDocumentStateResponse
            {
                IsSuccess = true
            };
        }
        private async Task<DocumentInfo> MapToDocumentInfoAsync(Models.Document document)
        {
            var creator = await _userRepository.RetrieveAsync(document.CreatorId);
            var category = await _categoryRepository.RetrieveAsync(document.CategoryId);
            var documentTags = await _documentsTagsRepository
                .RetrieveCollectionAsync(new DocumentTagFilter { DocumentId = document.DocumentId })
                .ToListAsync();

            var tags = new List<string>();
            foreach (var dt in documentTags)
            {
                var tag = await _tagRepository.RetrieveAsync(dt.TagId);
                if (tag != null)
                    tags.Add(tag.Name);
            }

            return new DocumentInfo
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                AccessLevel = document.AccessLevel,
                IsDeleted = document.IsDeleted,
                CreatorId = document.CreatorId,
                CreatorFirstName = creator?.FirstName,
                CreatorLastName = creator?.LastName,
                CategoryId = document.CategoryId,
                CategoryName = category?.Name,
                Tags = tags
            };
        }    
    }
}
