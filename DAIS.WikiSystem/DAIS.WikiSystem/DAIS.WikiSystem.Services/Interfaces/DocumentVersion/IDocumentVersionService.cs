using DAIS.WikiSystem.Services.DTOs.DocumentVersion;

namespace DAIS.WikiSystem.Services.Interfaces.DocumentVersion
{
    public interface IDocumentVersionService
    {
        Task<GetDocumentVersionResponse> GetByIdAsync(int documentVersionId);
        Task<GetAllDocumentVersionResponse> GetAllAsync(int documentId);
        Task<CreateDocumentVersionResponse> CreateDocumentVersionAsync(CreateDocumentVersionRequest request);
        Task<UpdateDocumentVersionResponse> UpdateDocumentVersionAsync(UpdateDocumentVersionRequest request);

    }
}
