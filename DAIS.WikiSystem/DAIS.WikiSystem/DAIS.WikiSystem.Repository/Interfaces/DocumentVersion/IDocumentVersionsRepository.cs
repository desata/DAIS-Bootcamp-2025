using DAIS.WikiSystem.Repository.Base;

namespace DAIS.WikiSystem.Repository.Interfaces.DocumentVersion
{
    public interface IDocumentVersionsRepository : IBaseRepository<Models.DocumentVersion, DocumentVersionFilter, DocumentVersionUpdate>
    {
    }
}
