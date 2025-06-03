using DAIS.WikiSystem.Repository.Base;

namespace DAIS.WikiSystem.Repository.Interfaces.DocumentVersion
{
    public interface IDocumentVersionRepository : IBaseRepository<Models.DocumentVersion, DocumentVersionFilter, DocumentVersionUpdate>
    {
    }
}
