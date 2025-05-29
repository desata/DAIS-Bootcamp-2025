using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikiSystem.Models;
using WikiSystem.Models.Enums;
using WikiSystem.Repository.Interfaces.Collection;
using WikiSystem.Repository.Interfaces.Document;
using WikiSystem.Repository.Interfaces.Employee;
using WikiSystem.Services.DTOs.Category;
using WikiSystem.Services.DTOs.Collection;
using WikiSystem.Services.DTOs.Document;
using WikiSystem.Services.Interfaces.Document;

namespace WikiSystem.Services.Implementation.Document
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<CreateDocumentResponse> CreateDocumentAsync(CreateDocumentRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllDocumentsRsponse> GetByAccessLevelAsync(int accessLevel)
        {
            var document = await _documentRepository.RetrieveAsync(accessLevel);
            if (document == null || !document.)
            {
                return new GetAllDocumentsRsponse
                {
                    Documents = new List<DocumentInfo>(),
                    TotalCount = 0
                };
            }
        }


        public async Task<GetAllDocumentsRsponse> GetByCategoryIdAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllDocumentsRsponse> GetByCreatorIdAsync(int creatorId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetDocumentResponse> GetByIdAsync(int documentId)
        {
            throw new NotImplementedException();
        }

        public async Task<GetAllDocumentsRsponse> GetByTitleAsync(string title)
        {
            throw new NotImplementedException();
        }


        private async Task<DocumentInfo> MapToDocumentInfo(Models.Document document)
        {

            return new DocumentInfo
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                AccessLevel = (AccessLevel)(document.AccessLevel),
                IsArchived = document.IsArchived,
                CreatorId = document.CreatorId,
                CategoryId = document.CategoryId
            };

        }
    }
}
