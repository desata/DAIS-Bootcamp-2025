using DAIS.WikiSystem.Services.DTOs.DocumentVersion;

namespace DAIS.WikiSystem.Services.Interfaces.DocumentVersion
{
    public interface IDocumentVersionService
    {
        Task<GetAllDocumentVersionByDocumentIdResponse> GetAllByDocumentIdAsync(int documentId);
        Task<GetActiveDocumentVersionByIdResponse> GetActiveVersionByIdAsync(int documentId);
        Task<CreateDocumentVersionResponse> CreateDocumentVersionAsync(CreateDocumentVersionRequest request);
    }
}
