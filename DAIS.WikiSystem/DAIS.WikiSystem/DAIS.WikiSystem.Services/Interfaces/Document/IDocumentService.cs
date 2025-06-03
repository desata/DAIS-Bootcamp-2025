using DAIS.WikiSystem.Models.Enums;
using DAIS.WikiSystem.Services.DTOs.Document;

namespace DAIS.WikiSystem.Services.Interfaces.Document
{
    public interface IDocumentService
    {
        Task<GetDocumentResponse> GetByIdAsync(int documentId);
        Task<GetAllDocumentsResponse> GetAllAsync();
        Task<GetAllDocumentsResponse> GetAllByCreatorIdAsync(int creator);
        Task<GetAllDocumentsResponse> GetAllActiveAsync(AccessLevel UserAccessLevel);
        Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request);
        Task<UpdateDocumentStateResponse> UpdateDocumentAsync(UpdateDocumentStateRequest request);
    }
}
