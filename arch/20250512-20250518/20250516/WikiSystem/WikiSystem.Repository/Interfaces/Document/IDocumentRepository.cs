using WikiSystem.Repository.Base;

namespace WikiSystem.Repository.Interfaces.Document
{
    public interface IDocumentRepository : IBaseRepository<Models.Document, DocumentFilter, DocumentUpdate>
    {
    }
}
