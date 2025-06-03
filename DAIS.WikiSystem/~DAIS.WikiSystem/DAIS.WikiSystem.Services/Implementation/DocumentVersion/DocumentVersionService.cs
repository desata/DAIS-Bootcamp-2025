using DAIS.WikiSystem.Repository.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Services.DTOs.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;

namespace DAIS.WikiSystem.Services.Implementation.DocumentVersion
{
    public class DocumentVersionService : IDocumentVersionService
    {
        private readonly IDocumentVersionsRepository _documentVersionRepository;
        public DocumentVersionService(IDocumentVersionsRepository documentVersionRepository)
        {
            _documentVersionRepository = documentVersionRepository;
        }

        public async Task<CreateDocumentVersionResponse> CreateDocumentVersionAsync(CreateDocumentVersionRequest request)
        {
            try
            {
                var activeVersions = await _documentVersionRepository
                    .RetrieveCollectionAsync(new DocumentVersionFilter
                    {
                        DocumentId = request.DocumentId,
                        IsArchived = true
                    })
                    .ToListAsync();

                foreach (var v in activeVersions)
                {
                    await _documentVersionRepository.UpdateAsync(v.DocumentVersionId, new DocumentVersionUpdate
                    {
                        IsArchived = true
                    });
                }

                var newDocVersion = new Models.DocumentVersion
                {
                    DocumentId = request.DocumentId,
                    Content = request.Content,
                    Version = request.Version,
                    CreateDate = DateTime.Now
                };

                int newId = await _documentVersionRepository.CreateAsync(newDocVersion);

                return new CreateDocumentVersionResponse
                {
                    Success = true,
                    DocumentVersionId = newId,
                };
            }
            catch (Exception ex)
            {
                return new CreateDocumentVersionResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<GetActiveDocumentVersionByIdResponse> GetActiveVersionByIdAsync(int documentId)
        {
            var documentVersion = await _documentVersionRepository
       .RetrieveCollectionAsync(new DocumentVersionFilter { DocumentId = documentId }).Where(v => v.IsArchived == false)
                .OrderByDescending(v => v.CreateDate).ToListAsync();

            var latest = documentVersion.FirstOrDefault();

            return (GetActiveDocumentVersionByIdResponse)MapToDocumentVersionInfo(latest);

        }

        public async Task<GetAllDocumentVersionByDocumentIdResponse> GetAllByDocumentIdAsync(int documentId)
        {

            var documentVersions = await _documentVersionRepository
                .RetrieveCollectionAsync(new DocumentVersionFilter { DocumentId = documentId })
                .ToListAsync();

            var response = new GetAllDocumentVersionByDocumentIdResponse
            {
                DocumentVersions = documentVersions.Select(MapToDocumentVersionInfo).ToList(),
                TotalCount = documentVersions.Count
            };

            return response;
        }

        private DocumentVersionInfo MapToDocumentVersionInfo(Models.DocumentVersion documentVersion)
        {

            return new DocumentVersionInfo
            {

                DocumentVersionId = documentVersion.DocumentVersionId,
                Content = documentVersion.Content,
                Version = documentVersion.Version,
                IsArchived = documentVersion.IsArchived,
                CreateDate = documentVersion.CreateDate,
                DocumentId = documentVersion.DocumentId
            };
        }
    }
}
