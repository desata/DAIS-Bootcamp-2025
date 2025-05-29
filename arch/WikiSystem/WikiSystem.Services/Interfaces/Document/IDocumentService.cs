using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiSystem.Services.DTOs.Document;

namespace WikiSystem.Services.Interfaces.Document
{
    public interface IDocumentService
    {
        Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request);
        Task<GetAllDocumentsRsponse> GetByCreatorIdAsync(int creatorId);
        Task<GetDocumentResponse> GetByIdAsync(int documentId);
        Task<GetAllDocumentsRsponse> GetByTitleAsync(string title);
        Task<GetAllDocumentsRsponse> GetByCategoryIdAsync(int categoryId);
        Task<GetAllDocumentsRsponse> GetByAccessLevelAsync(int accessLevel);


    }
}
