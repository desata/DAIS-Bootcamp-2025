using DAIS.WikiSystem.Repository.Base;

namespace DAIS.WikiSystem.Repository.Interfaces.Document
{
    public interface IDocumentRepository : IBaseRepository<Models.Document, DocumentFilter, DocumentUpdate>
    {
    }
}
