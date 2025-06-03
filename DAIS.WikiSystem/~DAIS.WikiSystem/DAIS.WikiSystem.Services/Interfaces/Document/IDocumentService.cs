using DAIS.WikiSystem.Services.DTOs.Document;

namespace DAIS.WikiSystem.Services.Interfaces.Document
{
    public interface IDocumentService
    {
        Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request);
        Task<GetAllDocumentsResponse> GetByCreatorIdAsync(int creatorId);
        Task<GetDocumentResponse> GetByIdAsync(int documentId);
        Task<GetAllDocumentsResponse> GetByTitleAsync(string title);
        Task<GetAllDocumentsResponse> GetByCategoryIdAsync(int categoryId);
        Task<GetAllDocumentsResponse> GetByAccessLevelAsync(int accessLevel);
    }
}