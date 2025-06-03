using DAIS.WikiSystem.Repository.Interfaces.DocumentVersion;
using DAIS.WikiSystem.Services.DTOs.DocumentVersion;
using DAIS.WikiSystem.Services.Interfaces.DocumentVersion;

namespace DAIS.WikiSystem.Services.Implementation.DocumentVersion
{
    public class DocumentVserionService : IDocumentVersionService
    {
        private readonly IDocumentVersionRepository _documentVersionRepository;


        public DocumentVserionService(IDocumentVersionRepository documentVersionRepository)
        {
            _documentVersionRepository = documentVersionRepository;

        }
        public async Task<CreateDocumentVersionResponse> CreateDocumentVersionAsync(CreateDocumentVersionRequest request)
        {
            var existingVersions = await _documentVersionRepository
                .RetrieveCollectionAsync(new DocumentVersionFilter { DocumentId = request.DocumentId })
                .ToListAsync();

            foreach (var version in existingVersions)
            {
                await UpdateDocumentVersionAsync(new UpdateDocumentVersionRequest
                {
                    DocumentVersionId = version.DocumentVersionId,
                    IsArchivedNewStatus = true
                });
            }

            int nextVersionNumber = existingVersions.Count + 1;
            string versionString = $"v{nextVersionNumber}.0";

            var newVersion = new Models.DocumentVersion
            {
                FilePath = request.FilePath,
                Version = versionString,
                DocumentId = request.DocumentId,
                CreateDate = DateTime.UtcNow,
                IsArchived = false
            };

            var newId = await _documentVersionRepository.CreateAsync(newVersion);

            return new CreateDocumentVersionResponse
            {
                DocumentVersionId = newId,
                FilePath = newVersion.FilePath,
                Version = newVersion.Version,
                IsArchived = newVersion.IsArchived,
                CreateDate = newVersion.CreateDate,
                IsSuccess = true
            };

        }

        public async Task<GetAllDocumentVersionResponse> GetAllAsync(int documentId)
        {
            var versions = await _documentVersionRepository
                .RetrieveCollectionAsync(new DocumentVersionFilter { DocumentId = documentId })
                .ToListAsync();

            var response = new GetAllDocumentVersionResponse
            {
                DocumentVersions = versions.Select(v => new GetDocumentVersionResponse
                {
                    DocumentVersionId = v.DocumentVersionId,
                    FilePath = v.FilePath,
                    Version = v.Version,
                    IsArchived = v.IsArchived,
                    CreateDate = v.CreateDate
                }).ToList(),
                Count = versions.Count
            };

            return response;
        }

        public async Task<GetDocumentVersionResponse> GetByIdAsync(int documentVersionId)
        {
            var version = await _documentVersionRepository.RetrieveAsync(documentVersionId);

            if (version == null)
                return null;

            return new GetDocumentVersionResponse
            {
                DocumentVersionId = version.DocumentVersionId,
                FilePath = version.FilePath,
                Version = version.Version,
                IsArchived = version.IsArchived,
                CreateDate = version.CreateDate
            };
        }


        public async Task<UpdateDocumentVersionResponse> UpdateDocumentVersionAsync(UpdateDocumentVersionRequest request)
        {
            try
            {
                var updateData = new DocumentVersionUpdate
                {
                    IsArchived = request.IsArchivedNewStatus
                };

                bool isUpdated = await _documentVersionRepository.UpdateAsync(request.DocumentVersionId, updateData);

                return new UpdateDocumentVersionResponse
                {
                    IsSuccess = isUpdated,
                    ErrorMessage = isUpdated ? null : "No version was updated."
                };
            }
            catch (Exception ex)
            {
                return new UpdateDocumentVersionResponse
                {
                    IsSuccess = false,
                    ErrorMessage = $"An error occurred while updating the document version: {ex.Message}"
                };
            }
        }
    }
}
